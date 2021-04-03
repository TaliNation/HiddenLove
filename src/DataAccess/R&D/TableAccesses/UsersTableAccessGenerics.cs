using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.TableAccesses
{
    public class UsersTableAccessGenerics : TableAccessGenerics
    {
        public override TEntity GetById<TKey, TEntity>(TKey key) =>
            QueryFactory.Query("users").Where("users.id", "=", key).FirstOrDefault<TEntity>();

        public override IEnumerable<TEntity> GetAll<TEntity>() =>
            QueryFactory.Query("users").Get<TEntity>();

        public override TKey Insert<TKey>(IEntity<TKey> entity) 
        {
            User user = (User)entity; 
            return QueryFactory.Query("users").InsertGetId<TKey>(new {
                emailaddress = user.EmailAddress,
                username = user.Username,
                fullusername = user.FullUsername,
                passwordhash = user.Passwordhash
            });
        }

        
    }
}