using SqlKata.Execution;

namespace HiddenLove.DataAccess.QueryFactories
{
    /// <summary>
    /// Configuration du créateur/exécuteur de requêtes SQL
    /// </summary>
    public interface IQueryFactory
    {
        QueryFactory QueryFactory { get; } 
    }
}