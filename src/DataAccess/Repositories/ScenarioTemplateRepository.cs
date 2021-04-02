using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class ScenarioTemplateRepository : Repository, IRead<int, ScenarioTemplate>, IInsert<int, ScenarioTemplate>
    {
        public virtual ScenarioTemplate GetById(int key) =>
            QueryFactory.Query("ScenarioTemplates").Where("ScenarioTemplates.Id", "=", key).FirstOrDefault<ScenarioTemplate>();

        public virtual IEnumerable<ScenarioTemplate> GetAll() =>
            QueryFactory.Query("ScenarioTemplates").Get<ScenarioTemplate>();
        
        public virtual int Insert(ScenarioTemplate entity) =>
            QueryFactory.Query("ScenarioTemplates").InsertGetId<int>(new {
                Title = entity.Title,
                Description = entity.Description,
            });
    }
}