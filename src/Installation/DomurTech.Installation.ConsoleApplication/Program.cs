using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

            Console.WriteLine(@"Sunucu: " + model.DataSource);
            Console.WriteLine(@"Veritabanı: " + model.InitialCatalog);
            Console.WriteLine(@"Kullanıcı Adı: " + model.UserId);
            Console.WriteLine(@"Şifre: " + model.Password);
            Step2Next();

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
                    var list = new List<string>();
                    _userInstaller = new UserInstaller(new Repository<User>(context), new Repository<Language>(context), new Repository<UserHistory>(context), new Repository<Person>(context), new Repository<PersonHistory>(context));
                    if (_userInstaller.Exists())
                    {
                        Step4();
                    }
                    list.AddRange(_userInstaller.Set(model));
                    
                    _roleInstaller = new RoleInstaller(new Repository<Role>(context), new Repository<RoleHistory>(context), new Repository<RoleLanguageLine>(context), new Repository<RoleLanguageLineHistory>(context), new Repository<Language>(context), new Repository<User>(context));

                    list.AddRange(_roleInstaller.Set());


                    _roleUserLineInstaller = new RoleUserLineInstaller(new Repository<RoleUserLine>(context), new Repository<RoleUserLineHistory>(context), new Repository<Role>(context), new Repository<User>(context));
                    list.AddRange(_roleUserLineInstaller.Set());

                    _actionInstaller = new ActionInstaller(new Repository<ERP.Data.Entities.Concrete.Action>(context));
                    list.AddRange(_actionInstaller.Set());

                    _applicationSettingInstaller = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context), new Repository<ApplicationSettingHistory>(context), new Repository<User>(context));
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
        }

        private static void Step4()
        {
           Console.WriteLine("step4");
            Console.ReadLine();
        }
    }
}
