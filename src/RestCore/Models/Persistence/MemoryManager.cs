using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace RestCore.Models.Persistence
{
    public class MemoryManager
    {
        private IMemoryCache memoryCacheField;

        public MemoryManager(IMemoryCache memoryCache)
        {
            memoryCacheField = memoryCache;
        }

        public object Get(string objectKey)
        {
            object memoryObject = null;
            if (!memoryCacheField.TryGetValue(objectKey, out memoryObject))
                return null;
            else
                return memoryObject;
        }
        public void Set(object memoryObject, string objectKey)
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(2));

            // Save data in cache.
            memoryCacheField.Set(objectKey, memoryObject, cacheEntryOptions);
        }
    }
}