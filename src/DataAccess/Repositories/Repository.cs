using System.Collections.Generic;
using System.Data.SqlClient;
using HiddenLove.Shared.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HiddenLove.DataAccess.Repositories
{
    public interface IRepository { }

    public interface IRead<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll();
    }

    public abstract class Repository : IRepository
    {
        protected QueryFactory QueryFactory { get; } 
        
        public Repository()
        {
            // string connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=hiddenlove_dev;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

            string connectionString = "Server=127.0.0.1;Port=5432;Database=hiddenlove_dev;User Id=postgres;Password=root;";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            
            var queryCompiler = new PostgresCompiler(); 

            QueryFactory = new QueryFactory(connection, new PostgresCompiler());
        }
    }
}