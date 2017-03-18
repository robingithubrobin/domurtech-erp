using System.Collections.Generic;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    public static class SystemRoles
    {
        private static readonly RoleProvider RoleProvider = new RoleProvider();

        public static List<Role> ActionRoles(string controller, string action)
        {
            return RoleProvider.Find(controller,action);
        }
   
    }
}