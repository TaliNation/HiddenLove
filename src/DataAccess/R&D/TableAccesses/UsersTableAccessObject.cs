using System;
using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.TableAccesses
{
    [Obsolete("")]
    public class UsersTableAccessObject : TableAccessObject
    {
        public override User GetById(object key) =>
            QueryFactory.Query("users").Where("users.id", "=", key).FirstOrDefault<User>();
        
        // public override IEnumerable<User> GetByColumn(string columnName, string columnValue) =>
        //     QueryFactory.Query("users").Where($"users.{columnName}", "=", columnValue).Get<User>();

        public override IEnumerable<User> GetAll() =>
            QueryFactory.Query("users").Get<User>();

        public override object Insert(object entity) 
        {
            User user = (User)entity; 
            return QueryFactory.Query("users").InsertGetId<object>(new {
                emailaddress = user.EmailAddress,
                username = user.Username,
                fullusername = user.FullUsername,
                passwordhash = user.Passwordhash
            });
        }

        
    }
}