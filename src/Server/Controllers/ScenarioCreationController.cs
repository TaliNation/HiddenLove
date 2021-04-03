using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HiddenLove.Shared.Models;
using HiddenLove.Shared.Models.ScenarioCreation;
using HiddenLove.DataAccess.RD.Repositories;
using HiddenLove.DataAccess.RD.TableAccesses;
using HiddenLove.Server.Helpers;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioCreationController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        [Route("Steps")]
        [Produces("application/json")]
        public IActionResult GetAllStepTemplates()
        { 
            var dbAccess = new DataAccess.RD.Repositories.Repository(new StepTemplatesTableAccess());
            
            IEnumerable<StepTemplate> entities =  dbAccess.GetAll<int, StepTemplate>();
            List<KeyValuePair<int, string>> res = entities.Select(x => new KeyValuePair<int, string>(x.Id, x.Title)).ToList<KeyValuePair<int, string>>();

            return Ok(res);
        }

        [Authorize]
        [HttpPost]
        [Route("NewScenario")]
        [Produces("application/json")]
        public IActionResult NewScenario(ScenarioCreation model)
        {
            var dbAccess = new DataAccess.RD.Repositories.Repository(new ScenarioTemplatesTableAccess());
            int scenarioTemplateId = dbAccess.Insert<int, ScenarioTemplate>(new ScenarioTemplate {
                Title = model.Title,
                Description = model.Description
            });

            dbAccess.SetTableAccess(new ScenarioTemplatesStepTemplatesTableAccess());

            foreach(ScenarioCreationStep step in model.Steps)
            {
                dbAccess.Insert<int, ScenarioTemplateStepTemplate>(new ScenarioTemplateStepTemplate {
                    IdScenariotemplate = scenarioTemplateId,
                    IdSteptemplate = step.StepId,
                    StartDate = step.StartTime,
                    EndDate = step.EndTime
                });
            }

            return Ok();
        }
    }
}