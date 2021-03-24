using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    public class UserRepository : Repository<int, User>
    {
        public UserRepository() : base() { }

        public override User GetById(int key) =>
            QueryFactory.Query("users").WhereColumns("users.id", "=", key.ToString()).First<User>();

        public User GetByEmailAddress(string emailAddress) =>
            QueryFactory.Query("users").WhereColumns("users.emailaddress", "=", emailAddress).First<User>();

        public override IEnumerable<User> GetAll() =>
            QueryFactory.Query("users").Get<User>();
    }
}