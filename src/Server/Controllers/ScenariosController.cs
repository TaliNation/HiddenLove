using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenariosController : ControllerBase
    {
        public IActionResult NewScenario()
        { 
            var dbAccess = new ScenarioRepository();
            return Ok(dbAccess.GetAll());
        }

        // public IActionResult CreateScenarioTemplate()
        // {
        //     var
        // }


    }
}