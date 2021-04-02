using System.Collections.Generic;
using System.Data.SqlClient;
using HiddenLove.DataAccess.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using HiddenLove.DataAccess.QueryFactories;

namespace HiddenLove.DataAccess.Repositories
{
    public interface IRead<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll();
    }

    public interface IInsert<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TKey Insert(TEntity entity);
    }

    public abstract class Repository
    {
        protected QueryFactory QueryFactory { get; } 
        
        public Repository()
        {
            IQueryFactory queryFactory = new ProductionDbQueryFactory();
            QueryFactory = queryFactory.QueryFactory;
        }

        public Repository(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
        }
    }
}