using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
	public sealed class FullScenarioTableAccess : SingleKeyDefaultTableAccess
	{
		protected override string TableName => "UserScenarios";
		protected override string PrimaryKeyName => "Id_Scenario";
	}
}