using System.Collections.Generic;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.TableAccess
{
    public abstract class TableAccess
    {
        protected QueryFactory QueryFactory;

        public void SetQueryFactory(QueryFactory queryFactory) =>
            QueryFactory = queryFactory;

        public abstract TEntity GetById<TKey, TEntity>(TKey key);

        public abstract IEnumerable<TEntity> GetAll<TEntity>();

        public abstract TKey InsertGetKey<TKey, TEntity>(TEntity entity);

        public abstract void Insert<TEntity>(TEntity entity);

        public abstract TEntity InsertGetEntity<TEntity>(TEntity entity);
    }
}