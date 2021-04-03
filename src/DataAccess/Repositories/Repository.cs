using System.Collections.Generic;
using System.Data.SqlClient;
using HiddenLove.DataAccess.Entities;
using SqlKata.Compilers;
using SqlKata.Execution;
using HiddenLove.DataAccess.QueryFactories;
using System;

namespace HiddenLove.DataAccess.Repositories
{
    [Obsolete("This module is part of an old data access framework and should not be used. Prefer the one in HiddenLove.DataAccess.RD.")]
    public interface IRead<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll();
    }

    [Obsolete("This module is part of an old data access framework and should not be used. Prefer the one in HiddenLove.DataAccess.RD.")]
    public interface IInsert<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        TKey Insert(TEntity entity);
    }

    [Obsolete("This module is part of an old data access framework and should not be used. Prefer the one in HiddenLove.DataAccess.RD.")]
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