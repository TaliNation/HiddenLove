using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HiddenLove.Shared.Models;
using HiddenLove.Shared.Models.ScenarioCreation;
using HiddenLove.Server.Helpers;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Shared.Enums;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenarioCreationController : ControllerBase
    {
        private User CurrentUser => (User)HttpContext.Items["User"];

        /// <summary>
        /// Récupération de tous les types d'étapes pour les assembler dans la création des scénarios
        /// </summary>
        [Authorize(CustomerTier.Normal)]
        [HttpGet]
        [Route("Steps")]
        [Produces("application/json")]
        public IActionResult GetAllStepTemplatesKeyValue()
        {
            var dbAccess = new Repository(new StepTemplatesTableAccess());

            IEnumerable<StepTemplate> entities =  dbAccess.GetAll<int, StepTemplate>();
            List<KeyValuePair<int, string>> res = entities.Select(x => new KeyValuePair<int, string>(x.Id, x.Title)).ToList();

            return Ok(res);
        }

        [Authorize(CustomerTier.Normal)]
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        public IActionResult GetAllStepTemplates()
        {
            var dbAccess = new Repository(new StepTemplatesTableAccess());

            IEnumerable<StepTemplate> entities = dbAccess.GetAll<int, StepTemplate>();

            var res = entities.Select(x => new {
                x.Id,
                x.Title,
                x.Id_FakeSpam
            });

            return Ok(res);
        }

        /// <summary>
        /// Création d'un nouveau scénario type
        /// </summary>
        [Authorize(CustomerTier.Normal)]
        [HttpPost]
        [Route("NewScenario")]
        [Produces("application/json")]
        public IActionResult NewScenario(ScenarioCreation model)
        {
            var dbAccess = new Repository(new ScenarioTemplatesTableAccess());

            int? idUser = CurrentUser.Id_Privilege == (int)CustomerTier.Admin ? null : CurrentUser.Id;

            int scenarioTemplateId = dbAccess.Insert<int, ScenarioTemplate>(new ScenarioTemplate {
                Image = model.Image,
                Title = model.Title,
                Description = model.Description,
                Id_User = idUser
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