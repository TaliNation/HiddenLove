using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.QueryFactories;
using HiddenLove.DataAccess.TableAccesses;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.Repositories
{

    /// <summary>
    /// Repository (dépôt) de base permettant d'aggréger toutes les requêtes simples vers la base de données.
    /// Voir <see cref="https://martinfowler.com/eaaCatalog/repository.html"/>
    /// </summary>
    public class Repository
    {
        protected QueryFactory QueryFactory { get; private set; } 

        public TableAccess TableAccess { get; private set; }
        
        public Repository(TableAccess tableAccess)
        {
            IQueryFactory queryFactory = new ProductionDbQueryFactory();
            QueryFactory = queryFactory.QueryFactory;

            SetTableAccess(tableAccess);
        }

        public Repository(TableAccess tableAccess, IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            SetTableAccess(tableAccess);
        }

        public void SetQueryFactory(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        public void SetTableAccess(TableAccess tableAccess)
        {
            TableAccess = tableAccess;
            TableAccess.SetQueryFactory(QueryFactory);
        }


        public virtual TEntity GetById<TKey, TEntity>(TKey key) where TEntity : IEntity<TKey> =>
            TableAccess.GetById<TKey, TEntity>(key);

        public virtual IEnumerable<TEntity> GetAll<TKey, TEntity>() where TEntity : IEntity<TKey> =>
            TableAccess.GetAll<TEntity>();

        public virtual IEnumerable<TEntity> GetByColumn<TKey, TEntity>(string columnName, object columnValue) where TEntity : IEntity<TKey> =>
            TableAccess.GetByColumn<TEntity>(columnName, columnValue);

        public virtual TKey Insert<TKey, TEntity>(TEntity entity) where TEntity : IEntity<TKey> =>
            TableAccess.Insert<TKey>(entity);
    }
}