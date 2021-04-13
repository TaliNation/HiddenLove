using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class ScenariosTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "Scenarios";
        protected override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity)
        {
            Scenario obj = (Scenario)entity;
            return QueryFactory.Query(TableName).InsertGetId<TKey>(new {
                Eventdate = obj.Eventdate,
                Id_scenariotemplate = obj.IdScenariotemplate,
                Id_user = obj.IdUser
            });
        }
    }
}