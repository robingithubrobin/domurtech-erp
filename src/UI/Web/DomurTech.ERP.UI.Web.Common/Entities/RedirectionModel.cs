using System;

namespace DomurTech.ERP.UI.Web.Common.Entities
{
    [Serializable]
    public class RedirectionModel
    {
        public string Message { get; set; }
        public string CssClass { get; set; }
        public string Url { get; set; }
        public int Timeout { get; set; }

    }
}
