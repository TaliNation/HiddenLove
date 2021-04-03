using System;
using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{
    [Obsolete("This module is part of an old data access framework and should not be used. Prefer the one in HiddenLove.DataAccess.RD.")]
    public class UserRepository : Repository, IRead<int, User>, IInsert<int, User>
    {
        //public UserRepository() : base() { }

        public virtual User GetById(int key) =>
            QueryFactory.Query("users").Where("users.id", "=", key).FirstOrDefault<User>();

        public virtual User GetByEmailAddress(string emailAddress) =>
            QueryFactory.Query("users").Where("users.emailaddress", "=", emailAddress).FirstOrDefault<User>();

        public virtual IEnumerable<User> GetAll() =>
            QueryFactory.Query("users").Get<User>();

        public virtual int Insert(User user) =>
            QueryFactory.Query("users").InsertGetId<int>(new {
                emailaddress = user.EmailAddress,
                username = user.Username,
                fullusername = user.FullUsername,
                passwordhash = user.Passwordhash
            });
        
        
    }
}