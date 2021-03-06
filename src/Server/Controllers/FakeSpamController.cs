using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HiddenLove.Server.Helpers;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.Shared.Models.SetFakeSpam;
using HiddenLove.Shared.Enums;

namespace HiddenLove.Server.Controllers
{
    [Authorize(CustomerTier.Admin)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FakeSpamController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetFakeAllSpams()
        {
            var dbAccess = new Repository(new FakeSpamsTableAccess());

            var entities = dbAccess.GetAll<int, FakeSpam>();

            var res = entities.Select(x => new KeyValuePair<int, string>(x.Id, x.Subject)).ToList();

            return Ok(res);
        }

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