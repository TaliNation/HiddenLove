using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class ScenarioTemplatesStepTemplatesTableAccess : TableAccess
    {
        public override string TableName => "ScenarioTemplates_StepTemplates";
        public override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity)
        {
            ScenarioTemplateStepTemplate obj = (ScenarioTemplateStepTemplate)entity;
            return _queryFactory.Query(TableName).InsertGetId<TKey>(new {
                Id_scenariotemplate = obj.IdScenariotemplate,
                Id_steptemplate = obj.IdSteptemplate,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate
            });
        }
    }
}