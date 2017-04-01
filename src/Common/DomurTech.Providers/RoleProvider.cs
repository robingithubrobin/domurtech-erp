using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomurTech.Providers.Abstract;
using DomurTech.Providers.Caching;
using DomurTech.Providers.DataAccess.EntityFramework;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    internal class RoleProvider : IDisposable
    {
        private bool _disposed;
        private IRepository<RoleActionLine> _repositoryRoleActionLine;
        public List<Role> Find(string controller, string action)
        {
            List<Role> list;
            var cacheKey = "DomurTech.Providers.RoleProvider.Find." + controller+"."+ action;
            ICacheManager cacheManager = new MemoryCacheManager();
            if (!cacheManager.Exists(cacheKey))
            {
                list = GetValueFromDatabase(controller,action);
                cacheManager.Add(cacheKey, list);
            }
            else
            {
                return cacheManager.Get<List<Role>>(cacheKey);
            }
            return list;
        }

        private List<Role> GetValueFromDatabase(string controller, string action)
        {
            using (var context = new DatabaseContext())
            {
                _repositoryRoleActionLine = new Repository<RoleActionLine>(context);
                var list = new List<Role>();
                var query = _repositoryRoleActionLine.Get().Include("Role");
                if (query == null) throw new Exception();
                var actionRoleLines = query.Where(e => e.Action.ControllerName == controller && e.Action.ActionName == action);
                foreach (var actionRoleLine in actionRoleLines)
                {
                    list.Add(actionRoleLine.Role);
                }
                context.Dispose();
                return list;
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
                    _repositoryRoleActionLine.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
