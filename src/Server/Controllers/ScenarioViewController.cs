using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Server.Helpers;
using HiddenLove.Shared.Models.ScenarioSelection;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using HiddenLove.Shared.Models;
using HiddenLove.Shared.Enums;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(CustomerTier.Free)]
    public class ScenarioViewController : ControllerBase
    {
        private User CurrentUser => (User)HttpContext.Items["User"];

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

        [HttpGet]
        [Route("UserList")]
        [Produces("application/json")]
        public IActionResult GetUserList()
        {
            var dbAccess = new Repository(new ScenarioScheduleTableAccess());
            IEnumerable<ScenarioSchedule> entities = dbAccess.GetByColumn<int, ScenarioSchedule>("IdUser", CurrentUser.Id);

            if(!entities.Any())
                return NotFound();

            var res = entities.Select(x => new UserScenarioData
            {
                Id = x.IdScenario,
                Title = x.Title,
                EventDate = x.EventDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            });

            return Ok(res);
        }

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