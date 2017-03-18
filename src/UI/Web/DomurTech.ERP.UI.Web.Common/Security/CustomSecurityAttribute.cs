using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using DomurTech.Core.Security;
using DomurTech.Globalization;
using DomurTech.Providers;

namespace DomurTech.ERP.UI.Web.Common.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomSecurityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = (Identity)Thread.CurrentPrincipal.Identity;
            if (!identity.IsAuthenticated)
            {
                if (identity.Roles.Count < 1)
                {
                    var routeValueDictionary = new RouteValueDictionary
                    {
                        {"controller", "Redirection"},
                        {"action", "RedirectAction"},
                        {"message", Messages.WarnLoginOrRegister},
                        {"cssClass", "alert alert-danger"},
                        {"timeout", 2},
                        {"url", "/"+identity.LanguageCode+"/Account/Login"}
                    };
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                }
                else
                {
                    var routeValueDictionary = new RouteValueDictionary
                    {
                        {"controller", "Redirection"},
                        {"action", "RedirectAction"},
                        {"message", Messages.WarnLoginOrRegister},
                        {"cssClass", "alert alert-danger"},
                        {"timeout", 2},
                        {"url", "/"+identity.LanguageCode+"/Account/Login"}
                    };
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                }
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            var identity = (Identity)Thread.CurrentPrincipal.Identity;
            var isAuthenticated = false;
            if (identity.IsAuthenticated)
            {
                var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var action = filterContext.ActionDescriptor.ActionName;
                var roles = SystemRoles.ActionRoles(controller,action);
                var roleGuids = new List<string>();
                if (roles != null)
                {
                    roleGuids.AddRange(roles.Select(role => role.RoleCode));
                }

                if (roleGuids.Count > 0)
                {
                    if (identity.Roles.Any(userRole => roleGuids.Contains(userRole)))
                    {
                        isAuthenticated = true;
                    }

                }
                else
                {
                    isAuthenticated = true;
                }
            }
            if (!isAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                       { "controller", "Redirection"},
                       { "action", "RedirectAction"},
                       { "message", Messages.DangerNoPermission},
                       { "cssClass", "alert alert-danger"},
                       { "timeout", 2},
                       { "url", "/"+identity.LanguageCode+"/Account/MyAccount"}
                   });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
