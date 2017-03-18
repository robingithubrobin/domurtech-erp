using System;
using System.Web;
using DomurTech.Core.Entities;
using DomurTech.ERP.UI.Web.Common.Entities.Abstract;
using DomurTech.Helpers;
using DomurTech.Providers;

namespace DomurTech.ERP.UI.Web.Common.Security.SessionBase
{
    public class WebSecurityManager : IWebSecurityManager
    {
        public void Set(Identity identity, DateTime expires, string key, bool rememberMe)
        {
            var serializedIdentity = SerializationHelper.SerializeToString(identity);
            var encryptedSerializedIdentity = serializedIdentity.Encrypt();
            HttpContext.Current.Session.Timeout = SystemSettings.SessionTimeOut;
            HttpContext.Current.Session[key] = encryptedSerializedIdentity;
        }
        public Principal Get(string key)
        {
            if (HttpContext.Current.Session == null)
            {
                return null;
            }

            if (HttpContext.Current.Session[key] == null) return null;
            var session = HttpContext.Current.Session[key];
            var value = session.ToString();
            if (string.IsNullOrEmpty(value)) return null;
            var decryptedValue = value.Decrypt();
            var identity = SerializationHelper.DeserializeFromString<Identity>(decryptedValue);
            var principle = new Principal(identity);
            return principle;
        }

        public void Remove(string key)
        {
            var session = HttpContext.Current.Session[key];
            HttpContext.Current.Session.Remove(key);
            if (session == null) return;
            HttpContext.Current.Session[key] = null;
        }
    }
}
