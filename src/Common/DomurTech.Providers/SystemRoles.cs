using System.Collections.Generic;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    public static class SystemRoles
    {
        public static List<Role> ActionRoles(string controller, string action)
        {
            using (var provider = new RoleProvider())
            {
                return provider.Find(controller, action);
            }
        }
    }
}