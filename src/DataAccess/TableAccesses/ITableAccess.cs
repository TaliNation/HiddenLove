using System.Collections.Generic;
using HiddenLove.DataAccess.Entities;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.TableAccesses
{
	public interface ITableAccess
	{
		void SetQueryFactory(QueryFactory queryFactory);
		TEntity GetById<TKey, TEntity>(TKey key);
		IEnumerable<TEntity> GetAll<TEntity>();
		IEnumerable<TEntity> GetByColumn<TEntity>(string columnName, object value);
		TKey Insert<TKey>(IEntity<TKey> entity);
		void Delete<TKey>(TKey key);
		void Update<TKey>(TKey key, IEntity<TKey> entity);
	}
}