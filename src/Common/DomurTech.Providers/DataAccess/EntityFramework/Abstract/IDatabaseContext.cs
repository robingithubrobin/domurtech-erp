using System;
using System.Data.Entity;

namespace DomurTech.Providers.DataAccess.EntityFramework.Abstract
{
    internal interface IDatabaseContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
    }
}
