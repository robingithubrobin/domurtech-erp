using System;
using System.Linq;
using DomurTech.Providers.Abstract;
using DomurTech.Providers.Caching;
using DomurTech.Providers.DataAccess.EntityFramework;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    internal class SettingProvider : IDisposable
    {
        private bool _disposed;
        private ICacheManager _cacheManager;
        private IRepository<Setting> _repositorySetting;
        public string GetValue(string key)
        {
            string value;
            if (key != "CacheTimeOut")
            {
                var cacheKey = "DomurTech.Common.Providers.SettingProvider.GetValueByKey." + key;
                _cacheManager= new MemoryCacheManager();
                if (!_cacheManager.Exists(cacheKey))
                {
                    value = GetValueFromDatabase(key);
                    _cacheManager.Add(key, value);
                }
                else
                {
                    return _cacheManager.Get<string>(cacheKey);
                }
            }
            else
            {
                value = GetValueFromDatabase(key);
            }
            return value;
        }

        private string GetValueFromDatabase(string key)
        {
            using (var context = new DatabaseContext())
            {
                _repositorySetting = new Repository<Setting>(context);
                return _repositorySetting.Get().Where(a => a.SettingKey == key).Select(b => b.SettingValue).FirstOrDefault();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositorySetting.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
