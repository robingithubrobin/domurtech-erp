﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using DomurTech.Core.Security;
using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Business.Managers.Concrete;
using DomurTech.ERP.Business.Managers.Options;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.UI.Web.Application.Models;
using DomurTech.ERP.UI.Web.Common.Entities;
using DomurTech.ERP.UI.Web.Common.Entities.Abstract;
using DomurTech.ERP.UI.Web.Common.Helpers;
using DomurTech.ERP.UI.Web.Common.Infrastructure;
using DomurTech.ERP.UI.Web.Common.Security;
using DomurTech.ERP.UI.Web.Common.Security.CookieBase;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Providers;


namespace DomurTech.ERP.UI.Web.Application.Controllers
{

    public class AccountController : CustomController
    {
        private readonly IWebSecurityManager _webSecurityManager = new WebSecurityManager();
        private IAccountManager AccountManager(IDatabaseContext context)
        {
            return new AccountManager(new Repository<User>(context), new Repository<UserHistory>(context), new Repository<RoleLanguageLine>(context), new Repository<Language>(context), new Repository<RoleUserLine>(context), new Repository<RoleUserLineHistory>(context), new Repository<Session>(context), new Repository<SessionHistory>(context));
        }

        public PartialViewResult UserSidebar()
        {
            using (var context = new DatabaseContext())
            {
                return PartialView(AccountManager(context).MyAccount());
            }

        }

        public PartialViewResult Header()
        {
            using (var context = new DatabaseContext())
            {
                var account = AccountManager(context).MyAccount();
                var model = new HeaderModel
                {
                    Languages = SystemLanguages.AllLanguages.Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList(),
                    ApplicationName = SystemSettings.ApplicationName,
                    Account = account
                };
                return PartialView(model);
            }
        }

        public ViewResult ForgotPassword()
        {
            using (var context = new DatabaseContext())
            {
                var model = AccountManager(context).ForgotPassword();
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    CaptchaHelper.SetCaptcha(model);
                    if (CaptchaHelper.HasError)
                    {
                        ModelState.AddModelError("CaptchaValue", CaptchaHelper.ErrorMessage);
                        return View(model);
                    }
                    AccountManager(context).ForgotPassword(model);

                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = Messages.InfoPasswordSentSuccesfully,
                        CssClass = "alert alert-success",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
                catch (CustomValidationException exception)
                {
                    var validationResult = exception.ValidationResult;
                    foreach (var t in validationResult)
                    {
                        ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                    }
                }
                catch (Exception exception)
                {
                    model.Message = exception.Message;
                }
                return View(model);
            }
        }

        public ViewResult SignUp()
        {
            using (var context = new DatabaseContext())
            {
                var business = AccountManager(context);
                var model = business.SignUp();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    CaptchaHelper.SetCaptcha(model);
                    if (CaptchaHelper.HasError)
                    {
                        ModelState.AddModelError("CaptchaValue", CaptchaHelper.ErrorMessage);
                        return View(model);
                    }
                    var business = AccountManager(context);
                    business.SignUp(model);

                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = Messages.InfoPasswordSentSuccesfully,
                        CssClass = "alert alert-success",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
                catch (CustomValidationException exception)
                {
                    var validationResult = exception.ValidationResult;
                    foreach (var t in validationResult)
                    {
                        ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                    }
                }
                catch (Exception exception)
                {
                    model.Message = exception.Message;
                }
                return View(model);
            }
        }

        [CustomSecurity]
        public ViewResult MyAccount()
        {
            using (var context = new DatabaseContext())
            {
                return View(AccountManager(context).MyAccount());
            }
        }

        public ViewResult Login()
        {
            using (var context = new DatabaseContext())
            {
                var model = AccountManager(context).Login();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    ReCaptchaHelper.Validate();
                    if (ReCaptchaHelper.HasError)
                    {
                        model = ReCaptchaHelper.GetModel(model);
                        ModelState.AddModelError("CaptchaValue", ReCaptchaHelper.ErrorMessage);
                        return View(model);
                    }
                    AccountManager(context).Login(model);
                    _webSecurityManager.Set(ControllerIdentity, DateTime.Now.AddMinutes(SystemSettings.SessionTimeOut), WebCommonConstants.PrivateKey, ControllerIdentity.RememberMe);

                    var principal = (Principal)Thread.CurrentPrincipal;
                    System.Web.HttpContext.Current.User = principal;
                    var redirectionModel = new RedirectionModel
                    {
                        Message = Messages.InfoLoginOperationSuccessful,
                        CssClass = "alert alert-success",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/MyAccount"
                    };

                    TempData["RedirectionModel"] = redirectionModel;

                    return RedirectToAction("Redirect", "Redirection");
                }
                catch (CustomValidationException exception)
                {
                    var validationResult = exception.ValidationResult;
                    foreach (var t in validationResult)
                    {
                        ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                    }
                }
                catch (Exception exception)
                {
                    model.Message = exception.Message;
                }
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            using (var context = new DatabaseContext())
            {
                AccountManager(context).Logout(LogoutOptions.ValidLogut);
                _webSecurityManager.Remove(WebCommonConstants.PrivateKey);
                var principle = (Principal)Thread.CurrentPrincipal;
                HttpContext.User = principle;
                TempData["RedirectionModel"] = new RedirectionModel
                {
                    Message = Messages.InfoLogoutOperationSuccessful,
                    CssClass = "alert alert-success",
                    Timeout = 2,
                    Url = "/" + ControllerIdentity.LanguageCode + "/Account/Login"
                };
                return RedirectToAction("Redirect", "Redirection");
            }
        }

        [CustomSecurity]
        public ActionResult UpdateMyPassword()
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    var model = AccountManager(context).UpdateMyPassword();
                    return View(model);
                }
                catch (NotFoundException exception)
                {
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = exception.Message,
                        CssClass = "alert alert-danger",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/MyAccount"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
            }
        }

        [CustomSecurity]
        [HttpPost]
        public ActionResult UpdateMyPassword(UpdatePasswordModel model)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    AccountManager(context).UpdateMyPassword(model);
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = Messages.InfoSaveOperationSuccessful,
                        CssClass = "alert alert-success",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/MyAccount"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
                catch (CustomValidationException exception)
                {
                    foreach (var t in exception.ValidationResult)
                    {
                        ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                    }
                }
                catch (NotFoundException exception)
                {
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = exception.Message,
                        CssClass = "alert alert-danger",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/UpdateMyPassword"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }

                catch (Exception exception)
                {
                    model.Message = exception.Message;
                }
                return View(model);
            }
        }

        [CustomSecurity]
        public ActionResult UpdateMyInformation()
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    var model = AccountManager(context).UpdateMyInformation();
                    return View(model);
                }
                catch (NotFoundException exception)
                {
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = exception.Message,
                        CssClass = "alert alert-danger",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/MyAccount"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
            }
        }

        [CustomSecurity]
        [HttpPost]
        public ActionResult UpdateMyInformation(UpdateInformationModel model)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    AccountManager(context).UpdateMyInformation(model);
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = Messages.InfoAccountInformationUpdatedSuccessfully,
                        CssClass = "alert alert-success",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/MyAccount"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }
                catch (CustomValidationException exception)
                {
                    foreach (var t in exception.ValidationResult)
                    {
                        ModelState.AddModelError("User." + t.PropertyName, t.ErrorMessage);
                    }
                }

                catch (NotFoundException exception)
                {
                    TempData["RedirectionModel"] = new RedirectionModel
                    {
                        Message = exception.Message,
                        CssClass = "alert alert-danger",
                        Timeout = 2,
                        Url = "/" + ControllerIdentity.LanguageCode + "/Account/UpdateMyInformation"
                    };
                    return RedirectToAction("Redirect", "Redirection");
                }

                catch (Exception exception)
                {
                    model.Message = exception.Message;
                }
                return View(model);
            }
        }


        public ActionResult TimeOutLogout()
        {
            using (var context = new DatabaseContext())
            {
                AccountManager(context).Logout(LogoutOptions.TimeOut);
                TempData["RedirectionModel"] = new RedirectionModel
                {
                    Message = Messages.WarnSessionTimeOut,
                    CssClass = "alert alert-danger",
                    Timeout = 2,
                    Url = "/" + ControllerIdentity.LanguageCode + "/Account/Login"
                };
                return RedirectToAction("Redirect", "Redirection");
            }
        }

       
       

       
    }
}