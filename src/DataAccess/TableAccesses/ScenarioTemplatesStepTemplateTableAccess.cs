using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class ScenariosTableAccess : TableAccess
    {
        public override string TableName => "Scenarios";
        public override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity)
        {
            Scenario obj = (Scenario)entity;
            return _queryFactory.Query(TableName).InsertGetId<TKey>(new {
                Eventdate = obj.Eventdate,
                Id_scenariotemplate = obj.IdScenariotemplate,
                Id_user = obj.IdUser
            });
        }
    }
}