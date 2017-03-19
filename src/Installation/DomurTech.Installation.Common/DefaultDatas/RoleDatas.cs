using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.DefaultDatas
{
    internal class RoleDatas
    {
        public List<RoleLanguageLine> RoleLanguageLines = new List<RoleLanguageLine>
        {
            new RoleLanguageLine {Role = new Role {RoleCode = "DEVELOPER"}, RoleName = "Geli�tirici"},
            new RoleLanguageLine {Role = new Role {RoleCode = "MANAGER"}, RoleName = "Y�netici"},
            new RoleLanguageLine {Role = new Role {RoleCode = "EDITOR"}, RoleName = "Edit�r"},
            new RoleLanguageLine {Role = new Role {RoleCode = "AUTHOR"}, RoleName = "Yazar"},
            new RoleLanguageLine {Role = new Role {RoleCode = "SUBSCRIBER"}, RoleName = "Abone"},
            new RoleLanguageLine {Role = new Role {RoleCode = "GUEST"}, RoleName = "Misafir"},
        };
    }
}