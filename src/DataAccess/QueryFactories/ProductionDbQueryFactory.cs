using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Configuration;

namespace HiddenLove.DataAccess.QueryFactories
{
    /// <summary>
    /// <see cref="IQueryFactory"/> correspondant à la base de production en ligne SQL Server.
    /// </summary>
    public class ProductionDbQueryFactory : IQueryFactory
    {
        public QueryFactory QueryFactory { get; }

        public ProductionDbQueryFactory()
        {
            var connection = new SqlConnection("Server=51.83.76.180;Database=HiddenLove;User Id=SA;Password=Azerty58!;");
            var queryCompiler = new SqlServerCompiler();

            QueryFactory = new QueryFactory(connection, queryCompiler);
        }
    }
}