using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.Models;
using DomurTech.Installation.Common.ValidationRules;
using DomurTech.Validation.FluentValidation;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Installation.Common.Installlers;

namespace DomurTech.Installation.ConsoleApplication
{
    internal class Program
    {
        private static LanguageInstaller _languageInstaller;
        private static UserInstaller _userInstaller;
        private static RoleInstaller _roleInstaller;
        private static RoleUserLineInstaller _roleUserLineInstaller;
        private static ActionInstaller _actionInstaller;
        private static ApplicationSettingInstaller _applicationSettingInstaller;
        private static void Main()
        {
            Step1();

        }

        private static void Step1()
        {
            Console.Write(@"Sunucu: ");
            var dataSource = Console.ReadLine();
            Console.Write(@"Veritabanı: ");
            var initialCatalog = Console.ReadLine();
            Console.Write(@"Kullanıcı Adı: ");
            var userId = Console.ReadLine();
            Console.Write(@"Şifre: ");
            var password = Console.ReadLine();

            var model = new DatabaseConnectionModel
            {
                DataSource = dataSource,
                InitialCatalog = initialCatalog,
                UserId = userId,
                Password = password
            };

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

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["InstallationDatabaseContext"].ConnectionString =
                    $"Data Source={model.DataSource}; Initial Catalog={model.InitialCatalog}; User ID={model.UserId}; Password={model.Password}";
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                Step2();
            }

            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                foreach (var t in validationResult)
                {
                    Console.WriteLine(t.ErrorMessage);
                }
            }

            catch (Exception exception)
            {
                model.Message = exception.ToString();
                Console.WriteLine(model.Message);
            }


        }

        private static void Step2()
        {
            var thread = new Thread(() =>
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

                using (var context = new InstallationDatabaseContext())
                {
                    _languageInstaller = new LanguageInstaller(new Repository<Language>(context));
                    if (_languageInstaller.Exists())
                    {
                        Step2Next();
                        var counter = 1;
                        var totalCount = _languageInstaller.GetList().Count;
                        foreach (var language in _languageInstaller.GetList())
                        {
                            _languageInstaller.Add(language);
                            Console.WriteLine(@"Language {0}/{1} {2}", counter, totalCount, language.LanguageCode);
                            counter++;
                        }
                    }
                }
                Console.WriteLine(@"Sunucu: " + model.DataSource);
                Console.WriteLine(@"Veritabanı: " + model.InitialCatalog);
                Console.WriteLine(@"Kullanıcı Adı: " + model.UserId);
                Console.WriteLine(@"Şifre: " + model.Password);
                Step2Next();
            });
            thread.Start();
        }

        private static void Step2Next()
        {
            while (true)
            {
                Console.WriteLine(@"Devam etmek istiyor musunuz? (E/H)");
                var input = Console.ReadLine();

                if (input != null)
                {
                    switch (input.ToUpper())
                    {
                        case "E":
                            Step3();
                            break;
                        case "H":
                            Step1();
                            break;
                        default:
                            continue;
                    }
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private static void Step3()
        {
            var thread = new Thread(() =>
            {
                Console.Write(@"Adı: ");
                var firstName = Console.ReadLine();
                Console.Write(@"Soyadı: ");
                var lastName = Console.ReadLine();
                Console.Write(@"Kullanıcı Adı: ");
                var username = Console.ReadLine();
                Console.Write(@"Şifre: ");
                var password = Console.ReadLine();
                Console.Write(@"Şifre Tekrarı: ");
                var confirmPassword = Console.ReadLine();
                Console.Write(@"Eposta: ");
                var email = Console.ReadLine();

                var model = new AdminModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    Password = password,
                    ConfirmPassword = confirmPassword,
                    Email = email
                };

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
                        _userInstaller = new UserInstaller(new Repository<User>(context));
                        if (_userInstaller.Exists())
                        {
                            Step4();
                        }

                        var counter = 1;
                        var totalCount = 1;
                        var addedUser= _userInstaller.Add(new User
                        {
                            Id = Guid.NewGuid(),
                            Username = model.Username,
                            Password = model.Password,
                            Email = model.Email,
                            CreateDate = DateTime.Now,
                            IsApproved = true,
                            DisplayOrder = 1,
                            Language = _languageInstaller.GetFirst(),
                            Person = new Person
                            {
                                Id = Guid.NewGuid(),
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                BirthDate = DateTime.Now.AddYears(-35),
                                TcKimlikNo = "12345678901",
                                CreateDate = DateTime.Now,
                                IsApproved = true,
                                DisplayOrder = 1
                            }
                        });
                        Console.WriteLine(@"User {0}/{1} {2}", counter, totalCount, model.Username);
                        _roleInstaller = new RoleInstaller(new Repository<User>(context),new Repository<Role>(context),new Repository<RoleHistory>(context));
                        totalCount = _roleInstaller.GetList().Count;    
                        foreach (var role in _roleInstaller.GetList())
                        {
                            var addedRole=_roleInstaller.Add(role);
                            Console.WriteLine(@"Role {0}/{1} {2}", counter, totalCount,addedRole.RoleCode);
                            counter++;
                        }

                        var listHistory = _roleInstaller.GetList(_roleInstaller.GetAll());
                        counter = 1;
                        totalCount = listHistory.Count;
                        foreach (var roleHistory in listHistory)
                        {
                          var added= _roleInstaller.Add(roleHistory);
                            Console.WriteLine(@"RoleHistory {0}/{1} {2}", counter, totalCount, added.RoleCode);
                            counter++;
                        }


                        list.AddRange(_roleInstaller.Set());


                        _roleUserLineInstaller = new RoleUserLineInstaller(new Repository<User>(context));
                        list.AddRange(_roleUserLineInstaller.Set());

                        _actionInstaller = new ActionInstaller();
                        list.AddRange(_actionInstaller.Set());

                        _applicationSettingInstaller = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context));
                        list.AddRange(_applicationSettingInstaller.Set());

                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }


                        Step4();
                    }

                }
                catch (CustomValidationException exception)
                {
                    var validationResult = exception.ValidationResult;
                    foreach (var t in validationResult)
                    {
                        Console.WriteLine(t.ErrorMessage);
                    }
                }

                catch (Exception exception)
                {
                    model.Message = exception.ToString();
                    Console.WriteLine(model.Message);
                }
                Console.ReadLine();
            });
            thread.Start();

            
        }

        private static void Step4()
        {
           Console.WriteLine("step4");
            Console.ReadLine();
        }
    }
}
