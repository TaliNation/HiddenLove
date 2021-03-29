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

    public interface IInsert<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TKey Insert(TEntity entity);
    }

    public abstract class Repository : IRepository
    {
        protected QueryFactory QueryFactory { get; } 
        
        public Repository()
        {
            string connectionString = "Server=127.0.0.1;Port=5432;Database=hiddenlove_dev;User Id=postgres;Password=root;";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            
            var queryCompiler = new PostgresCompiler(); 

            QueryFactory = new QueryFactory(connection, new PostgresCompiler());
        }
    }
}