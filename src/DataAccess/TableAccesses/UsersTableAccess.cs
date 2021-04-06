using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class UsersTableAccess : TableAccess
    {
        public override string TableName => "Users";
        public override string PrimaryKeyName => "Id";

        public override TKey Insert<TKey>(IEntity<TKey> entity) 
        {
            User user = (User)entity; 
            return QueryFactory.Query(TableName).InsertGetId<TKey>(new {
                emailaddress = user.EmailAddress,
                username = user.Username,
                fullusername = user.FullUsername,
                passwordhash = user.Passwordhash
            });
        }

        
    }
}