using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.UI.Web.Installation.Infrastructure.Installlers;
using DomurTech.ERP.UI.Web.Installation.Models;
using DomurTech.ERP.UI.Web.Installation.ValidationRules;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Validation.FluentValidation;

namespace DomurTech.ERP.UI.Web.Installation.Controllers
{
    public class HomeController : Controller
    {

        private LanguageInstaller _languageInstaller;
        private UserInstaller _userInstaller;
        private RoleInstaller _roleInstaller;
        private RoleUserLineInstaller _roleUserLineInstaller;
        private SettingInstaller _settingInstaller;
        private CountryInstaller _countryInstaller;
        private CityInstaller _cityInstaller;
        private DistrictInstaller _districtInstaller;
        private ActionInstaller _actionInstaller;
        public ViewResult Step1()
        {
            return View(new DatabaseConnectionModel());
        }

        [HttpPost]
        public ActionResult Step1(DatabaseConnectionModel model)
        {
            try
            {
                var validator = new FluentBaseValidator<DatabaseConnectionModel, DatabaseConnectionRules>(model);
                var validationResults = validator.Validate();
                if (!validator.IsValid)
                {
                    throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                    {
                        ValidationResult = validationResults
                    };
                }
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
                section.ConnectionStrings["InstallationDatabaseContext"].ConnectionString =
                    $"Data Source={model.DataSource}; Initial Catalog={model.InitialCatalog}; User ID={model.UserId}; Password={model.Password}";
                configuration.Save();
                return RedirectToAction("Step2");
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
                model.Message = exception.ToString();

            }
            return View(model);
        }

        public ViewResult Step2()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["InstallationDatabaseContext"].ConnectionString;
            var keyValuePairs = connectionString.Split(';').Select(s => s.Split('=')).Select(connectionStringArray => new KeyValuePair<string, string>(connectionStringArray[0].Trim(), connectionStringArray[1].Trim())).ToList();

            var model = new DatabaseConnectionModel
            {
                DataSource = keyValuePairs.FirstOrDefault(x => x.Key == "Data Source").Value,
                InitialCatalog = keyValuePairs.FirstOrDefault(x => x.Key == "Initial Catalog").Value,
                UserId = keyValuePairs.FirstOrDefault(x => x.Key == "User ID").Value,
                Password = keyValuePairs.FirstOrDefault(x => x.Key == "Password").Value,
                Message = ""
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult Step2(DatabaseConnectionModel model)
        {
            try
            {
                var validator = new FluentBaseValidator<DatabaseConnectionModel, DatabaseConnectionRules>(model);
                var validationResults = validator.Validate();
                if (!validator.IsValid)
                {
                    throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                    {
                        ValidationResult = validationResults
                    };
                }


                using (var context = new InstallationDatabaseContext())
                {
                    _languageInstaller=new LanguageInstaller(new Repository<Language>(context));
                    if (_languageInstaller.Exists())
                    {
                        return RedirectToAction("Step3");
                    }
                    _languageInstaller.Set();
                    return RedirectToAction("Step3");
                }
            }

            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                foreach (var t in validationResult)
                {
                    ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                }
            }

            catch (SqlException exception)
            {
                model.HasError = true;
                model.Message = "Veritabanına bağlanılamadı. Lütfen bilgilerinizi kontrol ediniz." + exception.Message;
            }

            return View(model);
        }

        public ViewResult Step3()
        {
            return View(new AdminModel());
        }


        [HttpPost]
        public ActionResult Step3(AdminModel model)
        {
            try
            {
                var validator = new FluentBaseValidator<AdminModel, AdminRules>(model);
                var validationResults = validator.Validate();
                if (!validator.IsValid)
                {
                    throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                    {
                        ValidationResult = validationResults
                    };
                }

                using (var context = new InstallationDatabaseContext())
                {

                    _userInstaller = new UserInstaller(new Repository<User>(context),new Repository<Language>(context),new Repository<UserHistory>(context));

                    if (_userInstaller.Exists())
                    {
                        return RedirectToAction("Step4");
                    }
                    _userInstaller.Set(model);

                    _roleInstaller = new RoleInstaller(new Repository<Role>(context), new Repository<RoleHistory>(context),new Repository<RoleLanguageLine>(context),new Repository<RoleLanguageLineHistory>(context),new Repository<Language>(context), new Repository<User>(context));
                    _roleInstaller.Set();
                    
                    _roleUserLineInstaller = new RoleUserLineInstaller(new Repository<RoleUserLine>(context), new Repository<RoleUserLineHistory>(context), new Repository<Role>(context), new Repository<User>(context));
                    _roleUserLineInstaller.Set();

                    _actionInstaller = new ActionInstaller(new Repository<Data.Entities.Concrete.Action>(context));
                    _actionInstaller.Set();

                    _settingInstaller = new SettingInstaller(new Repository<Setting>(context),new Repository<SettingHistory>(context),new Repository<User>(context));
                    _settingInstaller.Set();

                    return RedirectToAction("Step4");
                }

            }

            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                foreach (var t in validationResult)
                {
                    ModelState.AddModelError(t.PropertyName, t.ErrorMessage);
                }
            }


            catch (SqlException)
            {
                model.HasError = true;
                model.Message = "Yönetici bilgileri kaydedilemedi. Lütfen bilgilerinizi kontrol ediniz.";
            }


            return View(model);
        }
        public ViewResult Step4()
        {
            var model = new SettingModel();
            try
            {

                using (var context = new InstallationDatabaseContext())
                {
                    _settingInstaller = new SettingInstaller(new Repository<Setting>(context), new Repository<SettingHistory>(context), new Repository<User>(context));

                    model.ApplicationName = _settingInstaller.GetSetting("ApplicationName").SettingValue ?? "";
                    model.ApplicationUrl = _settingInstaller.GetSetting("ApplicationUrl").SettingValue ?? "";
                    model.SmtpServer = _settingInstaller.GetSetting("SmtpServer").SettingValue ?? "";
                    model.SmtpPort = _settingInstaller.GetSetting("SmtpPort").SettingValue ?? "";
                    model.SmtpSsl = _settingInstaller.GetSetting("SmtpSsl").SettingValue ?? "";
                    model.SmtpUser = _settingInstaller.GetSetting("SmtpUser").SettingValue ?? "";
                    model.SmtpPassword = _settingInstaller.GetSetting("SmtpPassword").SettingValue ?? "";
                    model.SmtpSenderName = _settingInstaller.GetSetting("SmtpSenderName").SettingValue ?? "";
                    model.SmtpSenderMail = _settingInstaller.GetSetting("SmtpSenderMail").SettingValue ?? "";
                }
            }
            catch (SqlException)
            {
                model.HasError = true;
                model.Message = "Veritabanına bağlanılamadı. Lütfen bilgilerinizi kontrol ediniz.";
            }

            catch (Exception)
            {
                model.HasError = true;
                model.Message = "Veritabanına bağlanılamadı. Lütfen bilgilerinizi kontrol ediniz.";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Step4(SettingModel model)
        {
            try
            {
                var validator = new FluentBaseValidator<SettingModel, SettingRules>(model);
                var validationResults = validator.Validate();
                if (!validator.IsValid)
                {
                    throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                    {
                        ValidationResult = validationResults
                    };
                }

                using (var context = new InstallationDatabaseContext())
                {

                    _settingInstaller = new SettingInstaller(new Repository<Setting>(context), new Repository<SettingHistory>(context), new Repository<User>(context));

                    var applicationName = _settingInstaller.GetSetting("ApplicationName");
                    if (applicationName != null)
                    {
                        applicationName.SettingValue = model.ApplicationName;
                        _settingInstaller.Update(applicationName);
                    }

                    var applicationUrl = _settingInstaller.GetSetting("ApplicationUrl");
                    if (applicationUrl != null)
                    {
                        applicationUrl.SettingValue = model.ApplicationUrl;
                        _settingInstaller.Update(applicationUrl);
                    }
                    var smtpServer = _settingInstaller.GetSetting("SmtpServer");
                    if (smtpServer != null)
                    {
                        smtpServer.SettingValue = model.SmtpServer;
                        _settingInstaller.Update(smtpServer);
                    }
                    var smtpPort = _settingInstaller.GetSetting("SmtpPort");
                    if (smtpPort != null)
                    {
                        smtpPort.SettingValue = model.SmtpPort;
                        _settingInstaller.Update(smtpPort);
                    }
                    var smtpSsl = _settingInstaller.GetSetting("SmtpSsl");
                    if (smtpSsl != null)
                    {
                        smtpSsl.SettingValue = model.SmtpSsl;
                        _settingInstaller.Update(smtpSsl);
                    }
                    var smtpUser = _settingInstaller.GetSetting("SmtpUser");
                    if (smtpUser != null)
                    {
                        smtpUser.SettingValue = model.SmtpUser;
                        _settingInstaller.Update(smtpUser);
                    }
                    var smtpPassword = _settingInstaller.GetSetting("SmtpPassword");
                    if (smtpPassword != null)
                    {
                        smtpPassword.SettingValue = model.SmtpPassword;
                        _settingInstaller.Update(smtpPassword);
                    }
                    var smtpSenderName = _settingInstaller.GetSetting("SmtpSenderName");
                    if (smtpSenderName != null)
                    {
                        smtpSenderName.SettingValue = model.SmtpSenderName;
                        _settingInstaller.Update(smtpSenderName);
                    }
                    var smtpSenderMail = _settingInstaller.GetSetting("SmtpSenderMail");
                    if (smtpSenderMail != null)
                    {
                        smtpSenderMail.SettingValue = model.SmtpSenderMail;
                        _settingInstaller.Update(smtpSenderMail);
                    }

                    _settingInstaller.SetHistory();


                    return RedirectToAction("Step5");
                }
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
                model.HasError = true;
                model.Message = "Genel ayarlar kaydedilemedi. Lütfen bilgilerinizi kontrol ediniz."+exception;
            }
            return View(model);
        }

        public ViewResult Step5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Step5(FormCollection form)
        {
            try
            {
                using (var context = new InstallationDatabaseContext())
                {
                    _countryInstaller = new CountryInstaller(new Repository<Country>(context), new Repository<CountryHistory>(context), new Repository<CountryLanguageLine>(context), new Repository<CountryLanguageLineHistory>(context), new Repository<Language>(context), new Repository<User>(context));
                    _countryInstaller.Set();

                    _cityInstaller = new CityInstaller(new Repository<City>(context), new Repository<CityHistory>(context), new Repository<CityLanguageLine>(context), new Repository<CityLanguageLineHistory>(context), new Repository<Language>(context), new Repository<User>(context), new Repository<Country>(context));
                    _cityInstaller.Set();


                    _districtInstaller = new DistrictInstaller(new Repository<District>(context), new Repository<DistrictHistory>(context), new Repository<DistrictLanguageLine>(context), new Repository<DistrictLanguageLineHistory>(context), new Repository<Language>(context), new Repository<User>(context), new Repository<City>(context));
                    _districtInstaller.Set();



                    return RedirectToAction("Step6");
                }

            }

            catch (Exception)
            {
                return View();
            }
            

        }

        public ViewResult Step6()
        {
            return View();
        }

        public ActionResult StepWizard(string step)
        {
            ViewBag.Step = step;
            return View();
        }
        
    }



}