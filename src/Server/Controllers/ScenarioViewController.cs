using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Server.Helpers;
using HiddenLove.Shared.Models.ScenarioSelection;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using Shared.Models.ScenarioView;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioViewController : ControllerBase
    {
        private User CurrentUser => (User)HttpContext.Items["User"];

        [Authorize]
        [HttpGet]
        [Route("Details/{id}")]
        [Produces("application/json")]
        public IActionResult GetScenarioDetails(int id)
        {
            var dbAccess = new Repository(new FullScenarioTableAccess());

            IEnumerable<FullScenario> entities = dbAccess.GetByColumn<int, FullScenario>("Id_Scenario", id);

            if(!entities.Any())
                return NotFound();

            var tempSharedInfo = entities.First();

            if(tempSharedInfo.Id_User != CurrentUser.Id)
                return Unauthorized();

            List<StepDetailsData> resStepsDetails = entities.Select(x => new StepDetailsData {
                Title = x.StepTitle,
                Description = x.StepDescription,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();

            var res = new ScenarioDetailsData
            {
                Title = tempSharedInfo.ScenarioTitle,
                Description = tempSharedInfo.ScenarioDescription,
                EventDate = tempSharedInfo.ScenarioEventDate,
                Steps = resStepsDetails
            };

            return Ok(res);
        }

        [Authorize]
        [HttpGet]
        [Route("UserList")]
        [Produces("application/json")]
        public IActionResult GetUserList()
        {
            var dbAccess = new Repository(new FullScenarioTableAccess());

            IEnumerable<FullScenario> entities = dbAccess.GetByColumn<int, FullScenario>("Id_User", CurrentUser.Id);

            if(!entities.Any())
                return NotFound();

            var res = entities.Select(x => new UserScenarioData {
                Id = x.Id_Scenario,
                Title = x.ScenarioTitle,
                EventDate = x.ScenarioEventDate
            }).DistinctBy(x => x.Id);

            return Ok(res);
        }

        [Authorize]
        [HttpDelete]
        [Route("Scenario/{id}")]
        [Produces("application/json")]
        public IActionResult DeleteScenario(int id)
        {
            var dbAccess = new Repository(new ScenariosTableAccess());
            dbAccess.Delete<int, Scenario>(id);

            return Ok();
        }
    }
}