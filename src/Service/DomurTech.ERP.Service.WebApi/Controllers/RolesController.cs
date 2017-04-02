using System;
using System.Web.Http;
using DomurTech.ERP.Business.Managers.Concrete;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Service.Common.Services;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Service.WebApi.Controllers
{
    public class RolesController : ApiController
    {

        public IHttpActionResult Get(Guid roleId, Guid languageId)
        {
            using (var context = new DatabaseContext())
            {
                using (var manager = new RoleManager(new Repository<Role>(context),new Repository<RoleHistory>(context),new Repository<User>(context),new Repository<RoleLanguageLine>(context)))
                {
                    using (var services = new RoleService(manager))
                    {
                        return Ok(services.Detail(roleId, languageId));
                    }
                }
            }
        }
    }
}
