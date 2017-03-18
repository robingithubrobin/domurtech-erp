using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DomurTech.ERP.Data.Access.EntityFramework.Abstract
{
    public interface IDatabaseContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
