using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class StepTemplatesRepository : Repository, IRead<int, Scenario>
    {
        public virtual Scenario GetById(int key) =>
            QueryFactory.Query("StepTemplates").Where("StepTemplates.Id", "=", key).FirstOrDefault<Scenario>();

        public virtual IEnumerable<Scenario> GetAll() =>
            QueryFactory.Query("StepTemplates").Get<Scenario>();
    }
}