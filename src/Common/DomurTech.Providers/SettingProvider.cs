using System.Linq;
using DomurTech.Providers.Abstract;
using DomurTech.Providers.Caching;
using DomurTech.Providers.DataAccess.EntityFramework;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    internal class SettingProvider
    {

        private readonly MemoryCacheManager _memoryCacheManager = new MemoryCacheManager();
        private IRepository<Setting> _repositorySetting;
        public string GetValue(string key)
        {
            string value;
            if (key != "CacheTimeOut")
            {
                var cacheKey = "DomurTech.Common.Providers.SettingProvider.GetValueByKey." + key;
                if (!_memoryCacheManager.Exists(cacheKey))
                {
                    value = GetValueFromDatabase(key);
                    _memoryCacheManager.Add(key, value);
                }
                else
                {
                    return _memoryCacheManager.Get<string>(cacheKey);
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
    }
}
