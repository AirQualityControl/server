using System.Threading.Tasks;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    /// <summary>
    ///     Interface that declare a set of method that might
    ///     be implemented by any other repository for additional flexibility
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        ///     Method that execute query from provided scheme and returns a result
        /// </summary>
        /// <param name="queryScheme">QueryScheme incoming scheme that will be converted to query and executed in db</param>
        /// <returns>An instance of QueryResult class</returns>
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
    }
}