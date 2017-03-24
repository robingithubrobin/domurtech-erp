namespace DomurTech.Providers
{
    public static class SystemSettings
    {
        public static string ApplicationName
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("ApplicationUrl");
                } 
            }
        }
        public static string ApplicationUrl
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("ApplicationUrl");
                }
            }
        }

        public static string SmtpServer
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpServer");
                }
            }
        }
        public static string SmtpPort
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpPort");
                }
            }
        }
        public static string SmtpSsl
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpSsl");
                }
            }
        }
        public static string SmtpUser
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpUser");
                }
            }
        }

        public static string SmtpPassword
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpPassword");
                }
            }
        }

        public static string SmtpSenderName
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpSenderName");
                }
            }
        }

        public static string SmtpSenderMail
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpSenderMail");
                }
            }
        }
        public static string SendMailAfterUpdateUserInformation
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterUpdateUserInformation");
                }
            }
        }
        public static string SendMailAfterUpdateUserPassword
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterUpdateUserPassword");
                }
            }
        }
        public static string SendMailAfterAddUser
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterAddUser");
                }
            }
        }
        public static string CaptchaLenght
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("CaptchaLenght");
                }
            }
        }
        public static string SessionTimeOut
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SessionTimeOut");
                }
            }
        }
        public static string PageSizeList
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("PageSizeList");
                }
            }
        }
        public static string DefaultPageSize
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("DefaultPageSize");
                }
            }
        }
        public static string CaptchaBackgroundImagePath
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("CaptchaBackgroundImagePath");
                }
            }
        }
        public static string EmailTemplatePath
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("EmailTemplatePath");
                }
            }
        }
        public static string CacheTimeOut
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("CacheTimeOut");
                }
            }
        }
        public static string UseLoginCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseLoginCaptcha");
                }
            }
        }
        public static string UseSignUpCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseSignUpCaptcha");
                }
            }
        }
        public static string UseForgotPasswordCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseForgotPasswordCaptcha");
                }
            }
        }
        public static string UserFiles
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UserFiles");
                }
            }
        }
        public static string DefaultLanguage
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("DefaultLanguage");
                }
            }
        }
        public static string ContentFiles
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("ContentFiles");
                }
            }
        }



       

    }
}
