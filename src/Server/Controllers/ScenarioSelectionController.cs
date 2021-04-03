using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.RD.TableAccesses;
using HiddenLove.Shared.Models.ScenarioSelection;
using Microsoft.AspNetCore.Mvc;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioSelectionController : ControllerBase
    {
        private User _currentUser => (User)HttpContext.Items["User"];

        [HttpGet]
        [Route("AvailableScenarios")]
        [Produces("application/json")]
        public IActionResult GetAvailableScenarios()
        {
            var dbAccess = new DataAccess.RD.Repositories.Repository(new ScenarioTemplatesTableAccess());

            IEnumerable<ScenarioTemplate> entities = dbAccess.GetAll<int, ScenarioTemplate>();
            List<ScenarioSelectionData> res = entities.Select(x => new ScenarioSelectionData {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToList<ScenarioSelectionData>();

            return Ok(res);
        }

        [HttpPost]
        [Route("BookScenario")]
        [Produces("application/json")]
        public IActionResult BookScenario(SelectedScenarioData model)
        {
            var dbAccess = new DataAccess.RD.Repositories.Repository(new ScenariosTableAccess());
            int scenarioId = dbAccess.Insert<int, Scenario>(new Scenario {
                IdScenariotemplate = model.IdScenario,
                IdUser = _currentUser.Id,
                Eventdate = model.EventDate
            });

            return Ok(scenarioId);
        }
    }
}