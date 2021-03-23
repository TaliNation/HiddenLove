using System.Data.SqlClient;
using SqlKata.Execution;
using SqlKata.Compilers;

namespace HiddenLove.DataAccess.Queries
{
    public abstract class BaseQuery
    {
        protected QueryFactory QueryFactory { get; }
        
        public BaseQuery()
        {
            var connection = new SqlConnection("User ID=postgres;Password=root;Host=localhost;Port=5432;Database=hiddenlove_dev;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
            QueryFactory = new QueryFactory(connection, new PostgresCompiler());
        }
    }
}