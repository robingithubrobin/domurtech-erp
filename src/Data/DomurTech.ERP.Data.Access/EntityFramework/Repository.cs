using System;
using System.Data.Entity;
using System.Linq;
using DomurTech.Core.Abstract;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;

namespace DomurTech.ERP.Data.Access.EntityFramework
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class, IEntity, new()
       
    {
        private bool _disposed;
        private readonly IDatabaseContext _context;

        public Repository(IDatabaseContext context)
        {
            _context = context;
        }
        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public T Add(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Added;
            return entry.Entity;
        }

        public T Update(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            return entry.Entity;
        }

        public void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
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
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
