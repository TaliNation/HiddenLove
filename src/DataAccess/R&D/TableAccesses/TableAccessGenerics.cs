using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.TableAccesses
{
    public abstract class TableAccessGenerics
    {
        protected QueryFactory QueryFactory;

        public void SetQueryFactory(QueryFactory queryFactory) =>
            QueryFactory = queryFactory;

        public abstract TEntity GetById<TKey, TEntity>(TKey key);

        public abstract IEnumerable<TEntity> GetAll<TEntity>();

        public abstract TKey Insert<TKey>(IEntity<TKey> entity);
    }
}