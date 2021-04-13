using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using HiddenLove.DataAccess.Exceptions;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
    /// <summary>
    /// Stratégie d'accès au données, permettant de cibler une table sur laquelle exécuter les requêtes, et changer sans reconstruire la connexion.
    /// Voir <see cref="https://fr.wikipedia.org/wiki/Strat%C3%A9gie_(patron_de_conception)"/> - <see cref="https://refactoring.guru/design-patterns/strategy"/>
    /// </summary>
    public abstract class SingleKeyDefaultTableAccess : ITableAccess
    {
        protected QueryFactory QueryFactory;

        /// <summary>
        /// Changement de constructeur/exécuteur de requêtes.
        /// </summary>
        public void SetQueryFactory(QueryFactory queryFactory) =>
            QueryFactory = queryFactory;

        /// <summary>
        /// Nom de la table ciblée dans la BDD. Doit être surchargé dans les sous-types.
        /// Insensible à la casse.
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Nom de la colonne ciblée dans la BDD (souvent "id"). Doit être surchargé dans les sous-types.
        /// Insensible à la casse.
        /// </summary>
        protected abstract string PrimaryKeyName { get; }

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