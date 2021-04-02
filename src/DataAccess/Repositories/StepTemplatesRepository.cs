using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class StepTemplateRepository : Repository, IRead<int, StepTemplate>
    {
        public virtual StepTemplate GetById(int key) =>
            QueryFactory.Query("StepTemplates").Where("StepTemplates.Id", "=", key).FirstOrDefault<StepTemplate>();

        public virtual IEnumerable<StepTemplate> GetAll() =>
            QueryFactory.Query("StepTemplates").Get<StepTemplate>();
    }
}