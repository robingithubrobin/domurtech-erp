using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.DefaultDatas
{
    internal class ApplicationSettingDatas
    {
        public List<ApplicationSetting> Settings = new List<ApplicationSetting>
        {
            new ApplicationSetting {SettingKey = "DefaultLanguage", SettingValue = "tr-TR"},
            new ApplicationSetting {SettingKey = "ApplicationName", SettingValue = "DomurTech ERP"},
            new ApplicationSetting {SettingKey = "ApplicationUrl", SettingValue = "http://firma.com"},
            new ApplicationSetting {SettingKey = "SmtpServer", SettingValue = "mail.firma.com"},
            new ApplicationSetting {SettingKey = "SmtpPort", SettingValue = "587"},
            new ApplicationSetting {SettingKey = "SmtpSsl", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "SmtpUser", SettingValue = "erp"},
            new ApplicationSetting {SettingKey = "SmtpPassword", SettingValue = "password"},
            new ApplicationSetting {SettingKey = "SmtpSenderName", SettingValue = "agea"},
            new ApplicationSetting {SettingKey = "SmtpSenderMail", SettingValue = "bilgi@firma.com"},
            new ApplicationSetting {SettingKey = "SendMailAfterUpdateUserInformation", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "SendMailAfterUpdateUserPassword", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "SendMailAfterAddUser", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "CaptchaLenght", SettingValue = "6"},
            new ApplicationSetting {SettingKey = "SessionTimeOut", SettingValue = "20"},
            new ApplicationSetting {SettingKey = "PageSizeList", SettingValue = "5,10,25,50,100,250,500"},
            new ApplicationSetting {SettingKey = "DefaultPageSize", SettingValue = "10"},
            new ApplicationSetting {SettingKey = "LuceneIndexDirectory", SettingValue = "/"},
            new ApplicationSetting {SettingKey = "CaptchaBackgroundImagePath", SettingValue = "/Assets/Images/CaptchaBackground.png"},
            new ApplicationSetting {SettingKey = "EmailTemplatePath", SettingValue = "Assets\\EmailTemplates"},
            new ApplicationSetting {SettingKey = "CacheTimeOut", SettingValue = "60"},
            new ApplicationSetting {SettingKey = "UseLoginCaptcha", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "UseSignUpCaptcha", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "UseForgotPasswordCaptcha", SettingValue = "false"},
            new ApplicationSetting {SettingKey = "UserFiles", SettingValue = "UserFiles"},
            new ApplicationSetting {SettingKey = "ContentFiles", SettingValue = "/ContentFiles"}


        };
    }
}