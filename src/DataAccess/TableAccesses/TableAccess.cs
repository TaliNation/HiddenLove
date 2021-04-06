using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Exceptions;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    public abstract class TableAccess
    {
        protected QueryFactory QueryFactory;

        public void SetQueryFactory(QueryFactory queryFactory) =>
            QueryFactory = queryFactory;

        public abstract string TableName { get; }
        public abstract string PrimaryKeyName { get; }

        public virtual TEntity GetById<TKey, TEntity>(TKey key) =>
            QueryFactory.Query(TableName).Where($"{TableName}.{PrimaryKeyName}", "=", key).FirstOrDefault<TEntity>();

        public virtual IEnumerable<TEntity> GetAll<TEntity>() =>
            QueryFactory.Query(TableName).Get<TEntity>();

        public virtual IEnumerable<TEntity> GetByColumn<TEntity>(string columnName, object columnValue) =>
            QueryFactory.Query(TableName).Where($"{TableName}.{columnName}", "=", columnValue).Get<TEntity>();

        public virtual TKey Insert<TKey>(IEntity<TKey> entity) =>
            throw new UnauthorizedQueryException("INSERT", TableName);
    }
}