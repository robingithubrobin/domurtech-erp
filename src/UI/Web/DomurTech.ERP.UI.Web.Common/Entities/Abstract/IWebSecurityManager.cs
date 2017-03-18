using System;
using DomurTech.Core.Security;

namespace DomurTech.ERP.UI.Web.Common.Entities.Abstract
{
    public interface IWebSecurityManager
    {
        void Set(Identity identity, DateTime expires, string key, bool rememberMe);
        Principal Get(string key);
        void Remove(string key);
    }
}
