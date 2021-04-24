using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AirSnitch.Core.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace AirSnitch.Infrastructure.Cache
{
    /// <summary>
    /// Class that store objects of specified type in application memory
    /// </summary>
    /// <typeparam name="TKey">Cache key type</typeparam>
    /// <typeparam name="TValue">Cache value type</typeparam>
    public class MemoryCacheStore<TKey, TValue> : ICacheStore<TKey, TValue>
        where TValue : ICloneable
    {
        private static readonly ConcurrentDictionary<TKey, TValue> CacheKeys = new ConcurrentDictionary<TKey, TValue>();
        public static readonly MemoryCacheStore<TKey, TValue> Instance = new MemoryCacheStore<TKey, TValue>();
        
        static MemoryCacheStore()
        {
            
        }

        private readonly MemoryCache _memoryCache;

        private MemoryCacheStore()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions { });
        }

        ///<inheritdoc/>
        public IEnumerable<TKey> Keys()
        {
            return CacheKeys.Keys;
        }

        ///<inheritdoc/>
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        ///<inheritdoc/>
        public TValue GetValue(TKey key)
        {
            if (!_memoryCache.TryGetValue(key, out TValue cacheResult))
            {
                CacheKeys.Remove(key, out _);
            }
            return GetCopy(cacheResult);
        }

        ///<inheritdoc/>
        public void SetValue(TKey key, TValue value)
        {
            AddEntryToCacheKey(key, value);
            _memoryCache.Set(key, value);
        }

        ///<inheritdoc/>
        public void SetValue(TKey key, TValue value, CacheEntryPolicy cacheEntryPolicy)
        {
            var copiedValue = GetCopy(value);
            AddEntryToCacheKey(key, copiedValue);
            _memoryCache.Set(key, copiedValue, cacheEntryPolicy.ToMemoryCacheEntryOptions());
        }
        
        ///<inheritdoc/>
        private void AddEntryToCacheKey(TKey key, TValue value)
        {
            if (!CacheKeys.TryAdd(key, value))
            {
                throw new Exception("unable to add item to cache");
            }
        }

        ///<inheritdoc/>
        public bool TryGetValue(TKey key, out TValue entry)
        {
            entry = default(TValue);
            if (!_memoryCache.TryGetValue(key, out entry))
            {
                CacheKeys.Remove(key, out _);
                return false;
            }
            entry = GetCopy(entry);
            return true;
        }
        
        private TValue GetCopy(TValue valueToCopy)
        {
            return (TValue) valueToCopy.Clone();
        }
    }
}