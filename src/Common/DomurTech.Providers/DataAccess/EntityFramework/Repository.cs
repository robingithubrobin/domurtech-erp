using System;
using System.Linq;
using DomurTech.Providers.Abstract;
using DomurTech.Providers.DataAccess.EntityFramework.Abstract;

namespace DomurTech.Providers.DataAccess.EntityFramework
{
    internal class Repository<T> : IDisposable, IRepository<T> where T : class, IEntity, new()
       
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
    }
}
