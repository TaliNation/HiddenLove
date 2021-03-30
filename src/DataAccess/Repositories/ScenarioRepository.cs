using System.Collections.Generic;
using HiddenLove.Shared.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class ScenarioRepository : Repository, IRead<int, Scenario>, IInsert<int, Scenario>
    {
        public virtual Scenario GetById(int key) =>
            QueryFactory.Query("scenarios").Where("scenarios.id", "=", key).FirstOrDefault<Scenario>();

        public virtual IEnumerable<Scenario> GetAll() =>
            QueryFactory.Query("scenarios").Get<Scenario>();

        public virtual int Insert(Scenario entity) =>
            QueryFactory.Query("scenarios").InsertGetId<int>(new {
                eventdate = entity.EventDate,
                id_scenariotemplate = entity.IdScenarioTemplate,
                id_user = entity.IdUser
            });
    }
}