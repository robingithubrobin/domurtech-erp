using DomurTech.Providers.Helpers;

namespace DomurTech.Providers
{
    public static class SystemSettings
    {
        private static readonly SettingProvider SettingProvider = new SettingProvider();
        public static string ApplicationName = SettingProvider.GetValue("ApplicationName");
        public static string ApplicationUrl = SettingProvider.GetValue("ApplicationUrl");
        public static string SmtpServer = SettingProvider.GetValue("SmtpServer");
        public static int SmtpPort = SettingProvider.GetValue("SmtpPort").ConvertToInt();
        public static bool SmtpSsl = SettingProvider.GetValue("SmtpSsl").ConvertToBoolean();
        public static string SmtpUser = SettingProvider.GetValue("SmtpUser");
        public static string SmtpPassword = SettingProvider.GetValue("SmtpPassword");
        public static string SmtpSenderName = SettingProvider.GetValue("SmtpSenderName");
        public static string SmtpSenderMail = SettingProvider.GetValue("SmtpSenderMail");
        public static bool SendMailAfterUpdateUserInformation = SettingProvider.GetValue("SendMailAfterUpdateUserInformation").ConvertToBoolean();
        public static bool SendMailAfterUpdateUserPassword = SettingProvider.GetValue("SendMailAfterUpdateUserPassword").ConvertToBoolean();
        public static bool SendMailAfterAddUser = SettingProvider.GetValue("SendMailAfterAddUser").ConvertToBoolean();
        public static int CaptchaLenght = SettingProvider.GetValue("CaptchaLenght").ConvertToInt();
        public static int SessionTimeOut => SettingProvider.GetValue("SessionTimeOut").ConvertToInt();
        public static string PageSizeList => SettingProvider.GetValue("PageSizeList");
        public static int DefaultPageSize => SettingProvider.GetValue("DefaultPageSize").ConvertToInt();
        public static string CaptchaBackgroundImagePath = SettingProvider.GetValue("CaptchaBackgroundImagePath");
        public static string EmailTemplatePath = SettingProvider.GetValue("EmailTemplatePath");
        public static int CacheTimeOut = SettingProvider.GetValue("CacheTimeOut").ConvertToInt();
        public static bool UseLoginCaptcha = SettingProvider.GetValue("UseLoginCaptcha").ConvertToBoolean();
        public static bool UseSignUpCaptcha = SettingProvider.GetValue("UseSignUpCaptcha").ConvertToBoolean();
        public static bool UseForgotPasswordCaptcha = SettingProvider.GetValue("UseForgotPasswordCaptcha").ConvertToBoolean();
        public static string UserFiles = SettingProvider.GetValue("UserFiles");
        public static string DefaultLanguage = SettingProvider.GetValue("DefaultLanguage");
        public static string ContentFiles = SettingProvider.GetValue("ContentFiles");

    }
}
