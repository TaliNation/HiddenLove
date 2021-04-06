using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace HiddenLove.DataAccess.QueryFactories
{
    public class ProductionDbQueryFactory : IQueryFactory
    {
        public QueryFactory QueryFactory { get; } 
        
        public ProductionDbQueryFactory()
        {
            SqlConnection connection = new SqlConnection("Server=51.83.76.180;Database=HiddenLove;User Id=SA;Password=Azerty58!;");
            var queryCompiler = new SqlServerCompiler(); 

            QueryFactory = new QueryFactory(connection, queryCompiler);
        }
    }
}