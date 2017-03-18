using System.Linq;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Access.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Get();
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        void Dispose();
    }
}
