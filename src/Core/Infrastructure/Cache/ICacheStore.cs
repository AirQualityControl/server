using System.Collections.Generic;

namespace AirSnitch.Core.Infrastructure.Cache
{
    /// <summary>
    /// Interface represent basic sets of operation that applicable to every generic-purpose cache store
    /// </summary>
    /// <typeparam name="TKey">TKey: cache key</typeparam>
    /// <typeparam name="TEntry">TEntry: cache entry type </typeparam>
    public interface ICacheStore<TKey, TEntry>
    {
        /// <summary>
        /// Returns and enumeration cache's keys
        /// </summary>
        /// <returns></returns>
        IEnumerable<TKey> Keys();

        TEntry this[TKey key] { get => GetValue(key); set => SetValue(key, value);}

        /// <summary>
        /// Get value from cache using specific key
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <returns>Cache entry that associated with provided key</returns>
        TEntry GetValue(TKey key);

        /// <summary>
        /// Try get value from cache by key not throwing exception
        /// </summary>
        /// <param name="key">cache entry key</param>
        /// <param name="entry">entity associated with key</param>
        /// <returns>bool that indicates whether operation was successful or not</returns>
        bool TryGetValue(TKey key, out TEntry entry);

        /// <summary>
        /// Set value to cache
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <param name="value">Cache entry that associated with provided key</param>
        void SetValue(TKey key, TEntry value);

        /// <summary>
        /// Set value to cache
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <param name="value">Cache entry that associated with provided key</param>
        /// <param name="cacheEntryPolicy">Policy that specifies behavior of cache entry: such as priority, expiration date etc..</param>
        void SetValue(TKey key, TEntry value, CacheEntryPolicy cacheEntryPolicy);
    }
} 