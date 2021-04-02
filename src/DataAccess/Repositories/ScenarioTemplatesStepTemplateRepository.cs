using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class ScenarioTemplatesStepTemplateRepository : Repository, IRead<int, ScenarioTemplatesStepTemplate>, IInsert<int, ScenarioTemplatesStepTemplate>
    {
        public virtual ScenarioTemplatesStepTemplate GetById(int id) =>
            QueryFactory.Query("ScenarioTemplates_StepTemplates").Where("ScenarioTemplates_StepTemplates.Id", "=", id).FirstOrDefault<ScenarioTemplatesStepTemplate>();

        public virtual IEnumerable<ScenarioTemplatesStepTemplate> GetAll() =>
            QueryFactory.Query("ScenarioTemplates_StepTemplates").Get<ScenarioTemplatesStepTemplate>();

        public virtual int Insert(ScenarioTemplatesStepTemplate entity) =>
            QueryFactory.Query("ScenarioTemplates_StepTemplates").InsertGetId<int>(new {
                Id_scenariotemplate = entity.IdScenariotemplate,
                Id_steptemplate = entity.IdSteptemplate,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            });
    }
}