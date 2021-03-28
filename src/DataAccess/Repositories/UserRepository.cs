using System.Collections.Generic;
using HiddenLove.Shared.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class UserRepository : Repository, IRead<int, User>
    {
        //public UserRepository() : base() { }

        public virtual User GetById(int key) =>
            QueryFactory.Query("users").Where("users.id", "=", key).First<User>();

        public virtual User GetByEmailAddress(string emailAddress) =>
            QueryFactory.Query("users").Where("users.emailaddress", "=", emailAddress).First<User>();

        public virtual IEnumerable<User> GetAll() =>
            QueryFactory.Query("users").Get<User>();
    }
}