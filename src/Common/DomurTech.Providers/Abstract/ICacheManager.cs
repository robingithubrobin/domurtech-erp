namespace DomurTech.Providers.Abstract
{
    internal interface ICacheManager
    {
        object Get(string key);
        T Get<T>(string key);
        bool Exists(string key);
        void Add(string key, object value);
        void Remove(string key);
    }
}
