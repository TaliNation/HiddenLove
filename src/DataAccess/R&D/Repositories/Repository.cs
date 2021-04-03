using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.QueryFactories;
using HiddenLove.DataAccess.RD.TableAccesses;
using HiddenLove.DataAccess.RD.TableInfos;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.Repositories
{

    public class Repository
    {
        protected QueryFactory QueryFactory { get; private set; } 

        public TableAccessGenerics TableAccess { get; private set; }
        
        public Repository(TableAccessGenerics tableAccess)
        {
            IQueryFactory queryFactory = new ProductionDbQueryFactory();
            QueryFactory = queryFactory.QueryFactory;

            SetTableAccess(tableAccess);
        }

        public Repository(TableAccessGenerics tableAccess, IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            SetTableAccess(tableAccess);
        }

        public void SetQueryFactory(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        public void SetTableAccess(TableAccessGenerics tableAccess)
        {
            TableAccess = tableAccess;
            TableAccess.SetQueryFactory(QueryFactory);
        }


        public virtual TEntity GetById<TKey, TEntity>(TKey key) where TEntity : IEntity<TKey> =>
            TableAccess.GetById<TKey, TEntity>(key);

        public virtual IEnumerable<TEntity> GetAll<TKey, TEntity>() where TEntity : IEntity<TKey> =>
            TableAccess.GetAll<TEntity>();

        public virtual TKey Insert<TKey, TEntity>(TEntity entity) where TEntity : IEntity<TKey> =>
            TableAccess.Insert<TKey>(entity);
    }
}