using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.DefaultDatas
{
    internal class RoleDatas
    {
        public List<RoleLanguageLine> RoleLanguageLines = new List<RoleLanguageLine>
        {
            new RoleLanguageLine {Role = new Role {RoleCode = "DEVELOPER"}, RoleName = "Geliþtirici"},
            new RoleLanguageLine {Role = new Role {RoleCode = "MANAGER"}, RoleName = "Yönetici"},
            new RoleLanguageLine {Role = new Role {RoleCode = "EDITOR"}, RoleName = "Editör"},
            new RoleLanguageLine {Role = new Role {RoleCode = "AUTHOR"}, RoleName = "Yazar"},
            new RoleLanguageLine {Role = new Role {RoleCode = "SUBSCRIBER"}, RoleName = "Abone"},
            new RoleLanguageLine {Role = new Role {RoleCode = "GUEST"}, RoleName = "Misafir"},
        };
    }
}