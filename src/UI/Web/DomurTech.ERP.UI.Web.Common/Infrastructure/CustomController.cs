using System.Threading;
using System.Web.Mvc;
using DomurTech.Core.Security;

namespace DomurTech.ERP.UI.Web.Common.Infrastructure
{
    public class CustomController : Controller
    {
        public Identity ControllerIdentity => (Identity)Thread.CurrentPrincipal.Identity;
    }
}
