using DomurTech.Providers.Helpers;

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
        public static int SmtpPort
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SmtpPort").ConvertToInt();
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
        public static bool SendMailAfterUpdateUserInformation
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterUpdateUserInformation").ConvertToBoolean();
                }
            }
        }
        public static bool SendMailAfterUpdateUserPassword
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterUpdateUserPassword").ConvertToBoolean();
                }
            }
        }
        public static bool SendMailAfterAddUser
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SendMailAfterAddUser").ConvertToBoolean();
                }
            }
        }
        public static int CaptchaLenght
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("CaptchaLenght").ConvertToInt();
                }
            }
        }
        public static int SessionTimeOut
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("SessionTimeOut").ConvertToInt();
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
        public static int DefaultPageSize
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("DefaultPageSize").ConvertToInt();
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
        public static int CacheTimeOut
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("CacheTimeOut").ConvertToInt();
                }
            }
        }
        public static bool UseLoginCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseLoginCaptcha").ConvertToBoolean();
                }
            }
        }
        public static bool UseSignUpCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseSignUpCaptcha").ConvertToBoolean();
                }
            }
        }
        public static bool UseForgotPasswordCaptcha
        {
            get
            {
                using (var settingProvider = new SettingProvider())
                {
                    return settingProvider.GetValue("UseForgotPasswordCaptcha").ConvertToBoolean();
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
