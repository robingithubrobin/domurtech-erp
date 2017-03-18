using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomurTech.Providers.Caching;
using DomurTech.Providers.DataAccess.EntityFramework;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    internal class RoleProvider
    {
        private readonly MemoryCacheManager _memoryCacheManager = new MemoryCacheManager();
        public List<Role> Find(string controller, string action)
        {
            List<Role> list;
            var cacheKey = "DomurTech.Providers.RoleProvider.Find." + controller+"."+ action;
            if (!_memoryCacheManager.Exists(cacheKey))
            {
                list = GetValueFromDatabase(controller,action);
                _memoryCacheManager.Add(cacheKey, list);
            }
            else
            {
                return _memoryCacheManager.Get<List<Role>>(cacheKey);
            }
            return list;
        }

        private List<Role> GetValueFromDatabase(string controller, string action)
        {
            var context = new DatabaseContext();
            var list = new List<Role>();
            var repositoryActionRoleLine = new Repository<RoleActionLine>(context);
            var query = repositoryActionRoleLine.Get().Include("Role");
            if (query == null) throw new Exception();
            var actionRoleLines = query.Where(e =>  e.Action.ControllerName == controller && e.Action.ActionName==action);
            foreach (var actionRoleLine in actionRoleLines)
            {
                list.Add(actionRoleLine.Role);
            }
            context.Dispose();
            return list;
        }
    }
}
