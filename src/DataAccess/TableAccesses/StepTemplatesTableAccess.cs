using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class StepTemplatesTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "StepTemplates";
        protected override string PrimaryKeyName => "Id";

		public override void Update<TKey>(TKey key, IEntity<TKey> entity)
		{
			var obj = (StepTemplate)entity;
			QueryFactory.Query(TableName).Where($"{TableName}.{PrimaryKeyName}", "=", key).Update(new {
				Id_fakespam = obj.Id_FakeSpam
			});
		}
    }
}