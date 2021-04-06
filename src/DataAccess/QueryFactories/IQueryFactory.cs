using SqlKata.Execution;

namespace HiddenLove.DataAccess.QueryFactories
{
    public interface IQueryFactory
    {
        QueryFactory QueryFactory { get; } 
    }
}