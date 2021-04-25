using System.Threading.Tasks;

namespace AirSnitch.Core.Infrastructure.Cache
{
    /// <summary>
    /// Interface represent basic sets of <b>asynchronous</b> operation that applicable to every generic-purpose cache store
    /// </summary>
    /// <typeparam name="TEntry">TEntry: cache entry type </typeparam>
    /// <typeparam name="TKey">TKey: cache key</typeparam>
    public interface IAsyncCacheStore<TKey, TEntry> : ICacheStore<TKey, TEntry>
    {
        /// <summary>
        /// Get value from cache asynchronously
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <returns>Cache entry that associated with provided key</returns>
        Task<TEntry> GetValueAsync(TKey key);
        
        /// <summary>
        /// Set value to cache asynchronously.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <param name="value">Cache entry that associated with provided key</param>
        /// <returns></returns>
        Task SetValueAsync(TKey key, TEntry value);
        
        /// <summary>
        /// Set value to cache asynchronously.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <param name="value">Cache entry that associated with provided key</param>
        /// <param name="cacheEntryPolicy">Policy that specifies behavior of cache entry: such as priority, expiration date etc..</param>
        /// <returns></returns>
        Task SetValueAsync(TKey key, TEntry value, CacheEntryPolicy cacheEntryPolicy);
    }
}