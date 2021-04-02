using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HiddenLove.Shared.Models;
using HiddenLove.Shared.Models.ScenarioCreation;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioCreationController : ControllerBase
    {

        
        [HttpGet]
        [Route("Steps")]
        [Produces("application/json")]
        public IActionResult GetAllStepTemplates()
        { 
            var dbAccess = new StepTemplateRepository();
            
            IEnumerable<StepTemplate> entities =  dbAccess.GetAll();
            List<KeyValuePair<int, string>> res = entities.Select(x => new KeyValuePair<int, string>(x.Id, x.Title)).ToList<KeyValuePair<int, string>>();

            return Ok(res);
        }

        [HttpPost]
        [Route("NewScenario")]
        [Produces("application/json")]
        public IActionResult NewScenario(ScenarioCreation model)
        {
            var scenarioTemplateDbAccess = new ScenarioTemplateRepository();
            int scenarioTemplateId = scenarioTemplateDbAccess.Insert(new ScenarioTemplate {
                Title = model.Title,
                Description = model.Description
            });

            var scenarioTemplateStepTemplateDbAccess = new ScenarioTemplatesStepTemplateRepository();

            foreach(ScenarioCreationStep step in model.Steps)
            {
                scenarioTemplateStepTemplateDbAccess.Insert(new ScenarioTemplatesStepTemplate {
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