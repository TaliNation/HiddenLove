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
        /// <summary>
        /// Constructeur et exécuteur des requêtes SQL
        /// </summary>
        protected QueryFactory QueryFactory { get; private set; }

        protected ITableAccess TableAccess { get; set; }

        /// <summary>
        /// Par défaut, accès à la base de production en ligne
        /// </summary>
        /// <param name="tableAccess">Stratégie d'accès à la BDD</param>
        public Repository(ITableAccess tableAccess)
        {
            IQueryFactory queryFactory = new ProductionDbQueryFactory();
            QueryFactory = queryFactory.QueryFactory;

            SetTableAccess(tableAccess);
        }

        /// <summary>Modification du QueryFactory</summary>
        /// <param name="tableAccess">Stratégie d'accès à la BDD</param>
        /// <param name="queryFactory">Constructeur et exécuteur des requêtes SQL</param>
        public Repository(ITableAccess tableAccess, IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            SetTableAccess(tableAccess);
        }

        /// <summary>
        /// Changement d'exécuteur des requêtes (pour cibler une autre BDD)
        /// </summary>
        public void SetQueryFactory(IQueryFactory queryFactory)
        {
            QueryFactory = queryFactory.QueryFactory;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        /// <summary>
        /// Changement de stratégie d'accès à la BDD (pour cibler une autre table)
        /// </summary>
        public void SetTableAccess(ITableAccess tableAccess)
        {
            TableAccess = tableAccess;
            TableAccess.SetQueryFactory(QueryFactory);
        }

        /// <summary>
        /// Récupération d'une entité par sa clef primaire
        /// </summary>
        /// <param name="key">Valeur de la clef primaire</param>
        /// <typeparam name="TKey">Type de la clef primaire (généralement Int32)</typeparam>
        /// <typeparam name="TEntity">
        /// Type de l'entité retournée par la requêtes. Doit correspondre au type spécifié dans le <see cref="TableAccesses.ITableAccess" />
        /// </typeparam>
        public virtual TEntity GetById<TKey, TEntity>(TKey key) where TEntity : IEntity<TKey> =>
            TableAccess.GetById<TKey, TEntity>(key);

        /// <summary>
        /// Récupération de toutes les entités d'une table
        /// </summary>
        /// <typeparam name="TKey">Type de la clef primaire (généralement Int32)</typeparam>
        /// <typeparam name="TEntity">
        /// Type de l'entité retournée par la requêtes. Doit correspondre au type spécifié dans le <see cref="TableAccesses.ITableAccess" />
        /// </typeparam>
        public virtual IEnumerable<TEntity> GetAll<TKey, TEntity>() where TEntity : IEntity<TKey> =>
            TableAccess.GetAll<TEntity>();

        /// <summary>
        /// Récupération des entités d'une table filtrées par une colonne
        /// </summary>
        /// <param name="columnName">Nom de la colonne dans la BDD</param>
        /// <param name="columnValue">Valeur sur laquelle filtrer la colonne</param>
        /// <typeparam name="TKey">Type de la clef primaire (généralement Int32)</typeparam>
        /// <typeparam name="TEntity">
        /// Type de l'entité retournée par la requêtes. Doit correspondre au type spécifié dans le <see cref="TableAccesses.ITableAccess" />
        /// </typeparam>
        public virtual IEnumerable<TEntity> GetByColumn<TKey, TEntity>(string columnName, object columnValue) where TEntity : IEntity<TKey> =>
            TableAccess.GetByColumn<TEntity>(columnName, columnValue);

        /// <summary>
        /// Insertion d'une donnée
        /// </summary>
        /// <param name="entity">Entité à insérer</param>
        /// <typeparam name="TKey">Type de la clef primaire (généralement Int32)</typeparam>
        /// <typeparam name="TEntity">
        /// Type de l'entité retournée par la requêtes. Doit correspondre au type spécifié dans le <see cref="TableAccesses.ITableAccess" />
        /// </typeparam>
        public virtual TKey Insert<TKey, TEntity>(TEntity entity) where TEntity : IEntity<TKey> =>
            TableAccess.Insert<TKey>(entity);
    }
}