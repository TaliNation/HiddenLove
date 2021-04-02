using System.Collections.Generic;
using HiddenLove.DataAccess.QueryFactories;
using HiddenLove.DataAccess.RD.TableAccess;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.Repositories
{

    public abstract class Repository
    {
        protected QueryFactory QueryFactory { get; private set; } 

        public TableAccess.TableAccess TableAccess { get; private set; }
        
        public Repository()
        {
            IQueryFactory queryFactory = new ProductionDbQueryFactory();
            QueryFactory = queryFactory.QueryFactory;
        }

        public Repository(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
        }

        public void SetQueryFactory(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        public void SetTableAccess(TableAccess.TableAccess tableAccess)
        {
            TableAccess = tableAccess;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        // public virtual TEntity GetById<TKey, TEntity>(TKey key) =>


        // IEnumerable<TEntity> GetAll<TEntity>() =>

        // TKey InsertGetKey<TKey, TEntity>(TEntity entity) =>

        // void Insert<TEntity>(TEntity entity) =>

        // TEntity InsertGetEntity<TEntity>(TEntity entity) =>

    }
}