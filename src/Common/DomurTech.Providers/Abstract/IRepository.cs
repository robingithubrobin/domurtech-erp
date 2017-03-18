using System.Linq;

namespace DomurTech.Providers.Abstract
{
    internal interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Get();
        void Dispose();
    }
}
