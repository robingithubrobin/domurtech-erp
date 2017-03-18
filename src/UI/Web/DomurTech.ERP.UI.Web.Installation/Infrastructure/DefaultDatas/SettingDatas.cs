using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas
{
    internal class SettingDatas
    {
        public List<Setting> Settings = new List<Setting>
        {
            new Setting {SettingKey = "DefaultLanguage", SettingValue = "tr-TR"},
            new Setting {SettingKey = "ApplicationName", SettingValue = "DomurTech ERP"},
            new Setting {SettingKey = "ApplicationUrl", SettingValue = "http://firma.com"},
            new Setting {SettingKey = "SmtpServer", SettingValue = "mail.firma.com"},
            new Setting {SettingKey = "SmtpPort", SettingValue = "587"},
            new Setting {SettingKey = "SmtpSsl", SettingValue = "false"},
            new Setting {SettingKey = "SmtpUser", SettingValue = "erp"},
            new Setting {SettingKey = "SmtpPassword", SettingValue = "password"},
            new Setting {SettingKey = "SmtpSenderName", SettingValue = "agea"},
            new Setting {SettingKey = "SmtpSenderMail", SettingValue = "bilgi@firma.com"},
            new Setting {SettingKey = "SendMailAfterUpdateUserInformation", SettingValue = "false"},
            new Setting {SettingKey = "SendMailAfterUpdateUserPassword", SettingValue = "false"},
            new Setting {SettingKey = "SendMailAfterAddUser", SettingValue = "false"},
            new Setting {SettingKey = "CaptchaLenght", SettingValue = "6"},
            new Setting {SettingKey = "SessionTimeOut", SettingValue = "20"},
            new Setting {SettingKey = "PageSizeList", SettingValue = "5,10,25,50,100,250,500"},
            new Setting {SettingKey = "DefaultPageSize", SettingValue = "10"},
            new Setting {SettingKey = "LuceneIndexDirectory", SettingValue = "/"},
            new Setting {SettingKey = "CaptchaBackgroundImagePath", SettingValue = "/Assets/Images/CaptchaBackground.png"},
            new Setting {SettingKey = "EmailTemplatePath", SettingValue = "Assets\\EmailTemplates"},
            new Setting {SettingKey = "CacheTimeOut", SettingValue = "60"},
            new Setting {SettingKey = "UseLoginCaptcha", SettingValue = "false"},
            new Setting {SettingKey = "UseSignUpCaptcha", SettingValue = "false"},
            new Setting {SettingKey = "UseForgotPasswordCaptcha", SettingValue = "false"},
            new Setting {SettingKey = "UserFiles", SettingValue = "UserFiles"},
            new Setting {SettingKey = "ContentFiles", SettingValue = "/ContentFiles"}


        };
    }
}