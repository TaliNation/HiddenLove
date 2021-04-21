namespace HiddenLove.DataAccess.TableAccesses
{
    public class ScenarioScheduleTableAccess : SingleKeyDefaultTableAccess
    {
		protected override string TableName => "ScenarioSchedule";

		protected override string PrimaryKeyName => "IdScenario";
    }
}