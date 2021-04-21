using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class UsersTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "Users";
        protected override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity)
        {
            User user = (User)entity;
            return QueryFactory.Query(TableName).InsertGetId<TKey>(new {
                emailaddress = user.EmailAddress,
                username = user.Username,
                fullusername = user.FullUsername,
                passwordhash = user.Passwordhash,
				id_privilege = user.Id_Privilege
            });
        }

		public override void Update<TKey>(TKey key, IEntity<TKey> entity)
		{
			var value = (User)entity;
			QueryFactory.Query(TableName).Where($"{TableName}.{PrimaryKeyName}", "=", key).Update(new {
				Id_privilege = value.Id_Privilege
			});
		}
    }
}