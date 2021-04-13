using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public class ScenarioTemplatesTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "ScenarioTemplates";
        protected override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity)
        {
            ScenarioTemplate obj = (ScenarioTemplate)entity;
            return QueryFactory.Query(TableName).InsertGetId<TKey>(new {
                Title = obj.Title,
                Description = obj.Description
            });
        }
    }
}