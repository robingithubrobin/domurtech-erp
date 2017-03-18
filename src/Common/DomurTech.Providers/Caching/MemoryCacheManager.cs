using System;
using System.Runtime.Caching;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Caching
{
    internal class MemoryCacheManager : ICacheManager
    {
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(60)
        };
        protected ObjectCache Cache => MemoryCache.Default;

        public object Get(string key)
        {
            return Cache[key];
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool Exists(string key)
        {
            return Cache.Contains(key);
        }

        public void Add(string key, object value)
        {
            if (value == null)
            {
                return;
            }
            if (Exists(key))
            {
                return;
            }
            AddForce(key, value);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        private void AddForce(string key, object value)
        {
            Cache.Add(new CacheItem(key, value), _policy);
        }
    }
}
