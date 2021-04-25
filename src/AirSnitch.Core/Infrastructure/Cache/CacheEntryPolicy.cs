using System;

namespace AirSnitch.Core.Infrastructure.Cache
{
    /// <summary>
    /// Describe policy for cache entry. 
    /// </summary>
    public class CacheEntryPolicy
    {
        /// <summary>
        /// Gets or sets a value that indicates whether a cache entry should be evicted at a specified point in time.
        /// </summary>
        public TimeSpan? AbsoluteExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether a cache entry should be evicted if it has not been accessed in a given span of time.
        /// </summary>
        public TimeSpan? SlidingExpirationTime { get; set; }
    }
}
