using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.ERP.UI.Web.Common.Entities;
using DomurTech.ERP.UI.Web.Common.Globalization;
using DomurTech.Helpers;
using DomurTech.Helpers.Options;
using DomurTech.Providers;

namespace DomurTech.ERP.UI.Web.Common.Helpers
{
    public static class CaptchaHelper
    {
        public static bool HasError { get; set; }

        public static string ErrorMessage { get; set; }

        private enum CaptchaCharOptions
        {
            Alpha,
            Numeric,
            Alphanumeric,
            AlphanumericPlus
        }

        private static Bitmap CreateCaptcha(string backgroundImagePath, int captchaLenght, CaptchaCharOptions captchaCharOptions)
        {
            string chars;
            switch (captchaCharOptions)
            {
                case CaptchaCharOptions.Alpha:
                    {
                        chars = "ABCDEFGHIJKLMNOPRSTUVYZQWXabcdefghijkmnoprstuvyzqwx";
                        break;
                    }

                case CaptchaCharOptions.Numeric:
                    {
                        chars = "0123456789";
                        break;
                    }
                case CaptchaCharOptions.Alphanumeric:
                    {
                        chars = "ABCDEFGHIJKLMNOPRSTUVYZQWXabcdefghijkmnoprstuvyzqwx0123456789";
                        break;
                    }

                case CaptchaCharOptions.AlphanumericPlus:
                    {
                        chars = "ABCDEFGHIJKLMNOPRSTUVYZQWXabcdefghijkmnoprstuvyzqwx0123456789/*-+()?";
                        break;
                    }
                default:
                    {
                        chars = "ABCDEFGHIJKLMNOPRSTUVYZQWXabcdefghijkmnoprstuvyzqwx0123456789";
                        break;
                    }
            }


            var code = "";
            var random = new Random();
            for (var i = 0; i < captchaLenght; i++)
            {
                var character = chars[random.Next(0, chars.Length - 1)];
                code += character;
            }
            var bitmap = (Bitmap)Image.FromFile(backgroundImagePath);
            var font = new Font("Times New Roman", 20, FontStyle.Bold);
            var textColor = Color.Gray;
            var textBackColor = Color.Transparent;
            HttpContext.Current.Session[WebCommonConstants.CaptchaKey] = code;
            return ImageHelper.AddTextToImage(bitmap, code, 50, DirectionOptions.Center, font, true, textColor, textBackColor, HatchStyle.Percent80);

        }

        public static void SetCaptcha(LoginModel model)
        {
            HttpContext.Current.Session[WebCommonConstants.CaptchaMessage] = null;

            // Captcha kullanılacak
            if (SystemSettings.UseLoginCaptcha)
            {
                if (HttpContext.Current.Session[WebCommonConstants.CaptchaKey] != null)
                {
                    var captchaValue = HttpContext.Current.Session[WebCommonConstants.CaptchaKey];

                    if (model.CaptchaValue != captchaValue.ToString())
                    {
                        HasError = true;
                        ErrorMessage = WebResources.CaptchaFailed;
                    }
                    else
                    {
                        HasError = false;
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = WebResources.CaptchaNotNull;
                }
                
            }
            //// Captcha yok
            else
            {
                HasError = false;
            }

            HttpContext.Current.Session[WebCommonConstants.CaptchaKey] = null;

        }

        public static void SetCaptcha(SignUpModel model)
        {
            HttpContext.Current.Session[WebCommonConstants.CaptchaMessage] = null;

            // Captcha kullanılacak
            if (SystemSettings.UseSignUpCaptcha)
            {
                if (HttpContext.Current.Session[WebCommonConstants.CaptchaKey] != null)
                {
                    var captchaValue = HttpContext.Current.Session[WebCommonConstants.CaptchaKey];

                    if (model.CaptchaValue != captchaValue.ToString())
                    {
                        HasError = true;
                        ErrorMessage = WebResources.CaptchaFailed;
                    }
                    else
                    {
                        HasError = false;
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = WebResources.CaptchaNotNull;
                }

            }
            //// Captcha yok
            else
            {
                HasError = false;
            }

            HttpContext.Current.Session[WebCommonConstants.CaptchaKey] = null;

        }

        public static void SetCaptcha(ForgotPasswordModel model)
        {
            HttpContext.Current.Session[WebCommonConstants.CaptchaMessage] = null;

            // Captcha kullanılacak
            if (SystemSettings.UseForgotPasswordCaptcha)
            {
                if (HttpContext.Current.Session[WebCommonConstants.CaptchaKey] != null)
                {
                    var captchaValue = HttpContext.Current.Session[WebCommonConstants.CaptchaKey];

                    if (model.CaptchaValue != captchaValue.ToString())
                    {
                        HasError = true;
                        ErrorMessage = WebResources.CaptchaFailed;
                    }
                    else
                    {
                        HasError = false;
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = WebResources.CaptchaNotNull;
                }

            }
            //// Captcha yok
            else
            {
                HasError = false;
            }

            HttpContext.Current.Session[WebCommonConstants.CaptchaKey] = null;

        }

        public static byte[] GetCaptchaBytes(string imgPath, int captchaLenght)
        {
            var bitmap = CreateCaptcha(imgPath, captchaLenght, CaptchaCharOptions.Numeric);
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public static LoginModel GetModel(LoginModel model)
        {
            model.Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList();
            return model;
        }
        public static SignUpModel GetModel(SignUpModel model)
        {
            model.Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList();
            return model;
        }

        public static ForgotPasswordModel GetModel(ForgotPasswordModel model)
        {
            model.Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList();
            return model;
        }
    }
}
