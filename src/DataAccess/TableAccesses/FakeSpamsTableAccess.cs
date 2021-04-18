using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public class FakeSpamsTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "FakeSpams";
		protected override string PrimaryKeyName => "Id";

		public override TKey Insert<TKey>(IEntity<TKey> entity)
		{
			FakeSpam obj = (FakeSpam)entity;

			return QueryFactory.Query(TableName).InsertGetId<TKey>(new {
				Name = obj.Name,
				Subject = obj.Subject,
				Body = obj.Body
			});
		}
    }
}