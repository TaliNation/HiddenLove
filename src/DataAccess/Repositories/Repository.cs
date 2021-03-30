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
            // string connectionString = "Server=127.0.0.1;Port=5432;Database=hiddenlove_dev;User Id=postgres;Password=root;";
            // NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            // var queryCompiler = new PostgresCompiler();

            SqlConnection connection = new SqlConnection("Server=51.83.76.180;Database=HiddenLove;User Id=SA;Password=Azerty58!;");
            var queryCompiler = new SqlServerCompiler(); 

            QueryFactory = new QueryFactory(connection, queryCompiler);
        }
    }
}