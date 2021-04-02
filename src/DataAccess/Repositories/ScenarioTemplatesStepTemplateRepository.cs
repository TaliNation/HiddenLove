using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class ScenarioTemplatesStepTemplateRepository : Repository
    {
        public virtual ScenarioTemplatesStepTemplate GetById(int scenarioTemplateKey, int stepTemplateKey) =>
            QueryFactory.Query("ScenarioTemplatesStepTemplates").Where("ScenarioTemplatesStepTemplates.Id_ScenarioTemplate", "=", scenarioTemplateKey).Where("StepTemplates.Id_ScenarioTemplate", "=", stepTemplateKey).FirstOrDefault<ScenarioTemplatesStepTemplate>();

        public virtual IEnumerable<ScenarioTemplatesStepTemplate> GetAll() =>
            QueryFactory.Query("ScenarioTemplatesStepTemplates").Get<ScenarioTemplatesStepTemplate>();

        public virtual void Insert(ScenarioTemplatesStepTemplate entity) =>
            QueryFactory.Query("ScenarioTemplatesStepTemplates").Insert(new {
                Id_scenariotemplate = entity.IdScenariotemplate,
                Id_steptemplate = entity.IdSteptemplate,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            });
    }
}