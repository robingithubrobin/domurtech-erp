using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.UI.Web.Installation.Models
{
    public class SettingModel : IBaseModel
    {
        public Guid Id { get; set; }
        public string ApplicationUrl { get; set; }
        public string ApplicationName { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpSsl { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpSenderName { get; set; }
        public string SmtpSenderMail { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }

    }
}