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
    public abstract class TableAccess
    {
        protected QueryFactory _queryFactory;

        /// <summary>
        /// Changement de constructeur/exécuteur de requêtes.
        /// </summary>
        public void SetQueryFactory(QueryFactory queryFactory) =>
            _queryFactory = queryFactory;

        /// <summary>
        /// Nom de la table ciblée dans la BDD. Doit être surchargé dans les sous-types.
        /// Insensible à la casse.
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// Nom de la colonne ciblée dans la BDD (souvent "id"). Doit être surchargé dans les sous-types.
        /// Insensible à la casse.
        /// </summary>
        public abstract string PrimaryKeyName { get; }

        public virtual TEntity GetById<TKey, TEntity>(TKey key) =>
            _queryFactory.Query(TableName).Where($"{TableName}.{PrimaryKeyName}", "=", key).FirstOrDefault<TEntity>();

        public virtual IEnumerable<TEntity> GetAll<TEntity>() =>
            _queryFactory.Query(TableName).Get<TEntity>();

        public virtual IEnumerable<TEntity> GetByColumn<TEntity>(string columnName, object columnValue) =>
            _queryFactory.Query(TableName).Where($"{TableName}.{columnName}", "=", columnValue).Get<TEntity>();

        public virtual TKey Insert<TKey>(IEntity<TKey> entity) =>
            throw new UnauthorizedQueryException("INSERT", TableName);
    }
}