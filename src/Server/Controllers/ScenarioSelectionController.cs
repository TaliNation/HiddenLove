using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Server.Helpers;
using HiddenLove.Shared.Enums;
using HiddenLove.Shared.Models.ScenarioSelection;
using Microsoft.AspNetCore.Mvc;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(CustomerTier.Basic)]
    public class ScenarioSelectionController : ControllerBase
    {
        private User CurrentUser => (User)HttpContext.Items["User"];

        /// <summary>
        /// Récupération de tous les scénarios type pour laisser le choix à l'utilisateur
        /// </summary>
        [HttpGet]
        [Route("AvailableScenarios")]
        [Produces("application/json")]
        public IActionResult GetAvailableScenarios()
        {
            var dbAccess = new Repository(new ScenarioTemplatesTableAccess());

            IEnumerable<ScenarioTemplate> entities = dbAccess.GetAll<int, ScenarioTemplate>();
            List<ScenarioSelectionData> res = entities.Where(x => !x.Id_User.HasValue).Select(x => new ScenarioSelectionData {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image
            }).ToList();

            return Ok(res);
        }

        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        [Authorize(CustomerTier.Normal)]
        public IActionResult GetPersonalizedScenarios()
        {
            var dbAccess = new Repository(new ScenarioTemplatesTableAccess());

            IEnumerable<ScenarioTemplate> entities = dbAccess.GetAll<int , ScenarioTemplate>();
            List<ScenarioSelectionData> res = entities.Where(x => x.Id_User == CurrentUser.Id).Select(x => new ScenarioSelectionData {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image
            }).ToList();

            return Ok(res);
        }

        /// <summary>
        /// Réservation et planification d'un scénario par l'utilisateur
        /// </summary>
        [HttpPost]
        [Route("BookScenario")]
        [Produces("application/json")]
        public IActionResult BookScenario(SelectedScenarioData model)
        {
            var dbAccess = new Repository(new ScenariosTableAccess());

            int scenarioId = dbAccess.Insert<int, Scenario>(new Scenario {
                IdScenariotemplate = model.IdScenario,
                IdUser = CurrentUser.Id,
                Eventdate = model.EventDate
            });

            return Ok(scenarioId);
        }

        /// <summary>
        /// API de test
        /// </summary>
        [HttpGet("CurrentUser")]
        [Produces("application/json")]
        public IActionResult GetCurrentUser()
        {
            return Ok(CurrentUser);
        }

        /// <summary>
        /// API de test
        /// </summary>
        [HttpGet("CurrentUsername")]
        [Produces("application/json")]
        public IActionResult GetCurrentUsername()
        {
            return Ok(CurrentUser.Id);
        }
    }
}