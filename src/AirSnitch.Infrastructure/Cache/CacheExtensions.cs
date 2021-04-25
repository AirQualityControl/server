using AirSnitch.Core.Infrastructure.Cache;


using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Contracts;

namespace AirSnitch.Infrastructure.Cache
{
    internal static class CacheExtensions
    {
        /// <summary>
        /// Convert <see cref="CacheEntryPolicy"/> to <see cref="Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions"/>
        /// </summary>
        /// <param name="cacheEntryPolicy">Cache entry policy to be converted to MemoryCacheEntryOptions.
        ///Precondition:
        ///    Parameter cannot be null
        /// </param>
        /// <returns>MemoryCacheEntryOptions result object with info from CacheEntryPolicy
        ///Postcondition:
        ///     Result is never null
        /// </returns>
        /// <exception cref="ContractViolationException">If cacheEntryPolicy is null - contractViolationException will be throw </exception>
        public static MemoryCacheEntryOptions ToMemoryCacheEntryOptions(this CacheEntryPolicy cacheEntryPolicy) 
        {
            Contract.Requires(cacheEntryPolicy != null);
            
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = cacheEntryPolicy.AbsoluteExpirationTime,
                SlidingExpiration = cacheEntryPolicy.SlidingExpirationTime
            };
        }
    }
}
