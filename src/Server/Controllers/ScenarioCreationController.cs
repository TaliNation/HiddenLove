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

        // [HttpGet]
        // [Route("NewScenario")]
        // [Produces("application/json")]
        // public IActionResult NewScenario()
        // {
        //     var dbAccess = new ScenarioTemplateRe();


        // }
    }
}