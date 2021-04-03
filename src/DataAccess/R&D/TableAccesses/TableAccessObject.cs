using System;
using System.Collections.Generic;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.RD.TableAccesses
{
    [Obsolete("")]
    public abstract class TableAccessObject
    {
        protected QueryFactory QueryFactory;

        public void SetQueryFactory(QueryFactory queryFactory) =>
            QueryFactory = queryFactory;

        public abstract object GetById(object key);

        public abstract IEnumerable<object> GetAll();

        public abstract object Insert(object entity);
    }
}