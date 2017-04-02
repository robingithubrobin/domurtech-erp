using System;
using System.Web.Http;
using DomurTech.ERP.Business.Managers.Concrete;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Service.Common.Services;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.Service.Entities.Concrete.RoleModels;
using DomurTech.Exceptions;

namespace DomurTech.ERP.Service.WebApi.Controllers
{
    public class RolesController : ApiController
    {
        [Route("roles")]
        [HttpGet]
        public IHttpActionResult Get(Guid? roleId, Guid? languageId)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    using (var manager = new RoleManager(new Repository<Role>(context), new Repository<RoleHistory>(context), new Repository<User>(context), new Repository<RoleLanguageLine>(context)))
                    {
                        using (var services = new RoleService(manager))
                        {
                            if (roleId != null && languageId!=null)
                            {
                                return Ok(services.Detail(roleId.Value, languageId.Value));
                            }
                            return BadRequest();

                        }
                    }
                }
            }
            catch (NotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Route("roles")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    using (var manager = new RoleManager(new Repository<Role>(context), new Repository<RoleHistory>(context), new Repository<User>(context), new Repository<RoleLanguageLine>(context)))
                    {
                        using (var services = new RoleService(manager))
                        {
                            return Ok(services.List(new ListModel()));
                        }
                    }
                }
            }
            catch (NotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
