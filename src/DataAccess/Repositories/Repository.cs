using System.Collections.Generic;
using System.Data.SqlClient;
using HiddenLove.DataAccess.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;

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
            string connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=hiddenlove_dev;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            SqlConnection connection = new SqlConnection(connectionString);

            var queryCompiler = new PostgresCompiler(); 

            QueryFactory = new QueryFactory(connection, new PostgresCompiler());
        }
    }
}