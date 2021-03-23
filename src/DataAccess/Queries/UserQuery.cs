using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Queries
{
    public class UserQuery : BaseQuery
    {
        public UserQuery() : base()
        { }

        public UserCredentials GetUserByEmailAddress(string emailAddress) =>
            QueryFactory.Query("Users").WhereColumns("Users.EmailAddress", "=", emailAddress).First<UserCredentials>();

        public User GetUserById(long id) =>
            QueryFactory.Query("Users").WhereColumns("Users.Id", "=", id.ToString()).First<User>();

        public IEnumerable<User> GetUsers() =>
            QueryFactory.Query("Users").Get<User>();
    }
}