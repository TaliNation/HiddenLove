using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HiddenLove.Shared.Models;
using HiddenLove.Shared.Models.ScenarioCreation;
using HiddenLove.Server.Helpers;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Shared.Models.SetFakeSpam;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FakeSpamController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetFakeAllSpams()
        {
            var dbAccess = new Repository(new FakeSpamsTableAccess());

            var entities = dbAccess.GetAll<int, FakeSpam>();

            var res = entities.Select(x => new KeyValuePair<int, string>(x.Id, x.Subject)).ToList();

            return Ok(res);
        }

        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateNewFakeSpam(NewFakeSpamRequest model)
        {
            var dbAccess = new Repository(new FakeSpamsTableAccess());

            int id = dbAccess.Insert<int, FakeSpam>(new FakeSpam {
                Name = model.Name,
                Subject = model.Subject,
                Body = model.Body
            });

            dbAccess.SetTableAccess(new StepTemplatesTableAccess());

            dbAccess.Update(model.StepTemplateId, new StepTemplate {
                Id_FakeSpam = id
            });

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult ChangeFakeSpam(UpdateStepTemplateFakeSpamRequest model)
        {
            var dbAccess = new Repository(new StepTemplatesTableAccess());

            dbAccess.Update(model.StepTemplateId, new StepTemplate {
                Id_FakeSpam = model.FakeSpamId
            });

            return Ok();
        }
    }
}