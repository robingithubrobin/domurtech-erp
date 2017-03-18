using System;
using System.Globalization;
using System.Threading;
using System.Web;
using DomurTech.Core.Security;
using DomurTech.ERP.UI.Web.Common.Entities.Abstract;
using DomurTech.Helpers;

namespace DomurTech.ERP.UI.Web.Common.Security.CookieBase
{
    public class WebSecurityManager : IWebSecurityManager
    {
        public void Set(Identity identity, DateTime expires, string key, bool rememberMe)
        {
            var serializedIdentity = SerializationHelper.SerializeToString(identity);
            var encryptedSerializedIdentity = serializedIdentity.Encrypt();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(key, encryptedSerializedIdentity)
            {
                Expires = expires
            });
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(identity.LanguageCode);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(identity.LanguageCode);
        }

        public Principal Get(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            var value = cookie?.Value;
            if (string.IsNullOrEmpty(value)) return null;
            var decryptedValue = value.Decrypt();
            var identity = SerializationHelper.DeserializeFromString<Identity>(decryptedValue);
            var principle = new Principal(identity);
            return principle;
        }

        public void Remove(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            HttpContext.Current.Response.Cookies.Remove(key);
            if (cookie == null) return;
            cookie.Expires = DateTime.Now.AddDays(-10);
            cookie.Value = null;
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }
}
