using AirSnitch.Core.Infrastructure.Cache;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;
using Microsoft.Extensions.Caching.Memory;

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
            Require.That(cacheEntryPolicy, Is.NotNull);
            
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = cacheEntryPolicy.AbsoluteExpirationTime,
                SlidingExpiration = cacheEntryPolicy.SlidingExpirationTime
            };
        }
    }
}
