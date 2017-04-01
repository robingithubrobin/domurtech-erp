using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Installation.Common.Installlers;
using DomurTech.Installation.Common.Models;
using DomurTech.Installation.Common.ValidationRules;
using DomurTech.Validation.FluentValidation;
using DomurTech.ERP.Data.Entities.Concrete;
using Action = DomurTech.ERP.Data.Entities.Concrete.Action;

namespace DomurTech.Installation.WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private LanguageInstaller _installerLanguage;
        private PersonInstaller _installerPerson;
        private UserInstaller _installerUser;
        private RoleInstaller _installerRole;
        private RoleUserLineInstaller _installerRoleUserLine;
        private ApplicationSettingInstaller _installerApplicationSetting;
        private CountryInstaller _installerCountry;
        private CityInstaller _installerCity;
        private DistrictInstaller _installerDistrict;
        private ActionInstaller _installerAction;
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
                    _installerLanguage=new LanguageInstaller(new Repository<Language>(context));
                    if (_installerLanguage.Exists())
                    {
                        return RedirectToAction("Step3");
                    }
                    foreach (var language in _installerLanguage.GetList())
                    {
                        _installerLanguage.Add(language);
                    }

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
            return View(new AdminModel
            {
                FirstName =  "Atıf",
                LastName = "DAĞ",
                Username = "atif.dag",
                Password = "DoT*2017+",
                ConfirmPassword = "DoT*2017+",
                Email = "atif.dag@gmail.com"
            });
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
                    _installerPerson = new PersonInstaller(new Repository<Person>(context),new Repository<PersonHistory>(context));

                    if (_installerPerson.Exists())
                    {
                        return RedirectToAction("Step4");
                    }

                    var person=_installerPerson.Add(new Person
                    {
                        Id = Guid.NewGuid(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        TcKimlikNo = "12345678901",
                        BirthDate = DateTime.Now.AddYears(-30),
                        DisplayOrder = 1,
                        CreateDate = DateTime.Now,
                        IsApproved = true
                    });
                    _installerUser = new UserInstaller(new Repository<User>(context), new Repository<UserHistory>(context));
                    _installerLanguage = new LanguageInstaller(new Repository<Language>(context));
                    var user = _installerUser.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Username = model.Username,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        Email = model.Email,
                        Person = person,
                        Language = _installerLanguage.GetFirst(),
                        DisplayOrder = 1,
                        IsApproved = true,
                        CreateDate = DateTime.Now
                    });


                    _installerPerson.Add(new PersonHistory
                    {
                        Id = Guid.NewGuid(),
                        PersonId = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        DisplayOrder = person.DisplayOrder,
                        IsApproved = person.IsApproved,
                        CreateDate = person.CreateDate,
                        CreatedBy = user.Id,
                        VersionNo = 1,
                        RestoreVersionNo = 0,
                        IsDeleted = false
                    });


                    _installerUser.Add(new UserHistory
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        PersonId = user.Person.Id,
                        LanguageId = user.Language.Id,
                        DisplayOrder = user.DisplayOrder,
                        IsApproved = user.IsApproved,
                        CreateDate = user.CreateDate,
                        CreatedBy = user.Id,
                        VersionNo = 1,
                        RestoreVersionNo = 0,
                        IsDeleted = false

                    });


                    _installerRole = new RoleInstaller(new Repository<User>(context),new Repository<Role>(context),new Repository<RoleHistory>(context),new Repository<RoleLanguageLine>(context),new Repository<RoleLanguageLineHistory>(context),new Repository<Language>(context) );

                    foreach (var role in _installerRole.GetRoleList())
                    {
                        _installerRole.AddRole(role);
                    }

                    var roles = _installerRole.GetAllRoles();


                    foreach (var roleHistory in _installerRole.GetRoleHistoryList(roles))
                    {
                        _installerRole.AddRoleHistory(roleHistory);
                    }

                    var roleLanguageLines = _installerRole.GetAllRoleLanguageLines();

                    foreach (var roleLanguageLine in _installerRole.GetRoleLanguageLineList(roles))
                    {
                        _installerRole.AddRoleLanguageLine(roleLanguageLine);
                    }

                    foreach (var roleLanguageLineHistory in _installerRole.GetRoleLanguageLineHistoryList(roleLanguageLines))
                    {
                        _installerRole.AddRoleLanguageLineHistory(roleLanguageLineHistory);
                    }


                    
                    _installerRoleUserLine = new RoleUserLineInstaller(new Repository<User>(context),new Repository<RoleUserLine>(context),new Repository<RoleUserLineHistory>(context),new Repository<Role>(context));

                    foreach (var roleUserLine in _installerRoleUserLine.GetRoleUserLineList())
                    {
                        _installerRoleUserLine.AddRoleUserLine(roleUserLine);
                    }


                    foreach (var roleUserLineHistory in _installerRoleUserLine.GetRoleUserLineHistoryList())
                    {
                        _installerRoleUserLine.AddRoleUserLineHistory(roleUserLineHistory);
                    }


                    _installerAction = new ActionInstaller(new Repository<Action>(context));

                    foreach (var action in _installerAction.GetActionList())
                    {
                        _installerAction.AddAction(action);
                    }


                    _installerApplicationSetting = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context),new Repository<ApplicationSettingHistory>(context) );

                    foreach (var applicationSetting in _installerApplicationSetting.GetApplicationSettingList())
                    {
                        _installerApplicationSetting.AddApplicationSetting(applicationSetting);
                    }
                    
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
            var model = new ApplicationSettingModel();
            try
            {

                using (var context = new InstallationDatabaseContext())
                {
                    _installerApplicationSetting = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context),new Repository<ApplicationSettingHistory>(context));

                    model.ApplicationName = _installerApplicationSetting.GetSetting("ApplicationName").SettingValue ?? "";
                    model.ApplicationUrl = _installerApplicationSetting.GetSetting("ApplicationUrl").SettingValue ?? "";
                    model.SmtpServer = _installerApplicationSetting.GetSetting("SmtpServer").SettingValue ?? "";
                    model.SmtpPort = _installerApplicationSetting.GetSetting("SmtpPort").SettingValue ?? "";
                    model.SmtpSsl = _installerApplicationSetting.GetSetting("SmtpSsl").SettingValue ?? "";
                    model.SmtpUser = _installerApplicationSetting.GetSetting("SmtpUser").SettingValue ?? "";
                    model.SmtpPassword = _installerApplicationSetting.GetSetting("SmtpPassword").SettingValue ?? "";
                    model.SmtpSenderName = _installerApplicationSetting.GetSetting("SmtpSenderName").SettingValue ?? "";
                    model.SmtpSenderMail = _installerApplicationSetting.GetSetting("SmtpSenderMail").SettingValue ?? "";
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
        public ActionResult Step4(ApplicationSettingModel model)
        {
            try
            {
                var validator = new FluentBaseValidator<ApplicationSettingModel, ApplicationSettingRules>(model);
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

                    _installerApplicationSetting = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context),new Repository<ApplicationSettingHistory>(context));

                    var applicationName = _installerApplicationSetting.GetSetting("ApplicationName");
                    if (applicationName != null)
                    {
                        applicationName.SettingValue = model.ApplicationName;
                        _installerApplicationSetting.Update(applicationName);
                    }

                    var applicationUrl = _installerApplicationSetting.GetSetting("ApplicationUrl");
                    if (applicationUrl != null)
                    {
                        applicationUrl.SettingValue = model.ApplicationUrl;
                        _installerApplicationSetting.Update(applicationUrl);
                    }
                    var smtpServer = _installerApplicationSetting.GetSetting("SmtpServer");
                    if (smtpServer != null)
                    {
                        smtpServer.SettingValue = model.SmtpServer;
                        _installerApplicationSetting.Update(smtpServer);
                    }
                    var smtpPort = _installerApplicationSetting.GetSetting("SmtpPort");
                    if (smtpPort != null)
                    {
                        smtpPort.SettingValue = model.SmtpPort;
                        _installerApplicationSetting.Update(smtpPort);
                    }
                    var smtpSsl = _installerApplicationSetting.GetSetting("SmtpSsl");
                    if (smtpSsl != null)
                    {
                        smtpSsl.SettingValue = model.SmtpSsl;
                        _installerApplicationSetting.Update(smtpSsl);
                    }
                    var smtpUser = _installerApplicationSetting.GetSetting("SmtpUser");
                    if (smtpUser != null)
                    {
                        smtpUser.SettingValue = model.SmtpUser;
                        _installerApplicationSetting.Update(smtpUser);
                    }
                    var smtpPassword = _installerApplicationSetting.GetSetting("SmtpPassword");
                    if (smtpPassword != null)
                    {
                        smtpPassword.SettingValue = model.SmtpPassword;
                        _installerApplicationSetting.Update(smtpPassword);
                    }
                    var smtpSenderName = _installerApplicationSetting.GetSetting("SmtpSenderName");
                    if (smtpSenderName != null)
                    {
                        smtpSenderName.SettingValue = model.SmtpSenderName;
                        _installerApplicationSetting.Update(smtpSenderName);
                    }
                    var smtpSenderMail = _installerApplicationSetting.GetSetting("SmtpSenderMail");
                    if (smtpSenderMail != null)
                    {
                        smtpSenderMail.SettingValue = model.SmtpSenderMail;
                        _installerApplicationSetting.Update(smtpSenderMail);
                    }

                    foreach (var applicationSettingHistory in _installerApplicationSetting.GetApplicationSettingHistoryList())
                    {
                        _installerApplicationSetting.AddApplicationSettingHistory(applicationSettingHistory);
                    }



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
                    _installerCountry = new CountryInstaller(new Repository<User>(context),new Repository<Country>(context),new Repository<CountryHistory>(context),new Repository<CountryLanguageLine>(context),new Repository<CountryLanguageLineHistory>(context),new Repository<Language>(context));

                    foreach (var country in _installerCountry.GetCountryList())
                    {
                        _installerCountry.AddCountry(country);
                    }

                    var countries = _installerCountry.GetAllCountries();


                    foreach (var countryHistory in _installerCountry.GetCountryHistoryList(countries))
                    {
                        _installerCountry.AddCountryHistory(countryHistory);
                    }


                    foreach (var countryLanguageLine in _installerCountry.GetCountryLanguageLineList(countries))
                    {
                        _installerCountry.AddCountryLanguageLine(countryLanguageLine);
                    }

                    var countryLanguageLines = _installerCountry.GetAllCountryLanguageLines();

                    foreach (var countryLanguageLineHistory in _installerCountry.GetCountryLanguageLineHistoryList(countryLanguageLines))
                    {
                        _installerCountry.AddCountryLanguageLineHistory(countryLanguageLineHistory);
                    }

                    _installerCity = new CityInstaller(new Repository<User>(context), new Repository<City>(context), new Repository<CityHistory>(context), new Repository<CityLanguageLine>(context), new Repository<CityLanguageLineHistory>(context), new Repository<Language>(context),new Repository<Country>(context));

                    foreach (var city in _installerCity.GetCityList())
                    {
                        _installerCity.AddCity(city);
                    }

                    var cities = _installerCity.GetAllCities();

                    foreach (var cityHistory in _installerCity.GetCityHistoryList(cities))
                    {
                        _installerCity.AddCityHistory(cityHistory);
                    }


                    foreach (var cityLanguageLine in _installerCity.GetCityLanguageLineList(cities))
                    {
                        _installerCity.AddCityLanguageLine(cityLanguageLine);
                    }

                    var cityLanguageLines = _installerCity.GetAllCityLanguageLines();

                    foreach (var cityLanguageLineHistory in _installerCity.GetCityLanguageLineHistoryList(cityLanguageLines))
                    {
                        _installerCity.AddCityLanguageLineHistory(cityLanguageLineHistory);
                    }

                    _installerDistrict = new DistrictInstaller(new Repository<User>(context), new Repository<District>(context), new Repository<DistrictHistory>(context), new Repository<DistrictLanguageLine>(context), new Repository<DistrictLanguageLineHistory>(context), new Repository<Language>(context),new Repository<City>(context));

                    foreach (var district in _installerDistrict.GetDistrictList())
                    {
                        _installerDistrict.AddDistrict(district);
                    }

                    var districts = _installerDistrict.GetAllDistricts();

                    foreach (var districtHistory in _installerDistrict.GetDistrictHistoryList(districts))
                    {
                        _installerDistrict.AddDistrictHistory(districtHistory);
                    }


                    foreach (var districtLanguageLine in _installerDistrict.GetDistrictLanguageLineList(districts))
                    {
                        _installerDistrict.AddDistrictLanguageLine(districtLanguageLine);
                    }
                    var districtLanguageLines = _installerDistrict.GetAllDistrictLanguageLines();
                    foreach (var districtLanguageLineHistory in _installerDistrict.GetDistrictLanguageLineHistoryList(districtLanguageLines))
                    {
                        _installerDistrict.AddDistrictLanguageLineHistory(districtLanguageLineHistory);
                    }



                    return RedirectToAction("Step6");
                }

            }

            catch (Exception exception)
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