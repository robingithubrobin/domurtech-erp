using System.Web.Http;
using Newtonsoft.Json;

namespace DomurTech.ERP.Service.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
      //      config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}