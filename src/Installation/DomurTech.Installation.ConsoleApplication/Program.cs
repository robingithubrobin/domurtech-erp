using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.Models;
using DomurTech.Installation.Common.ValidationRules;
using DomurTech.Validation.FluentValidation;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Installation.Common.Installlers;
using Action = DomurTech.ERP.Data.Entities.Concrete.Action;

namespace DomurTech.Installation.ConsoleApplication
{
    internal class Program
    {
        private static readonly IDatabaseContext Context=new DatabaseContext();
        private static readonly IRepository<Language> RepositoryLanguage=new Repository<Language>(Context);
        private static readonly IRepository<Person> RepositoryPerson = new Repository<Person>(Context);
        private static readonly IRepository<PersonHistory> RepositoryPersonHistory = new Repository<PersonHistory>(Context);
        private static readonly IRepository<User> RepositoryUser = new Repository<User>(Context);
        private static readonly IRepository<UserHistory> RepositoryUserHistory = new Repository<UserHistory>(Context);
        private static readonly IRepository<Role> RepositoryRole = new Repository<Role>(Context);
        private static readonly IRepository<RoleHistory> RepositoryRoleHistory = new Repository<RoleHistory>(Context);
        private static readonly IRepository<RoleUserLine> RepositoryRoleUserLine = new Repository<RoleUserLine>(Context);
        private static readonly IRepository<RoleUserLineHistory> RepositoryRoleUserLineHistory = new Repository<RoleUserLineHistory>(Context);
        private static readonly IRepository<ApplicationSetting> RepositoryApplicationSetting = new Repository<ApplicationSetting>(Context);
        private static readonly IRepository<ApplicationSettingHistory> RepositoryApplicationSettingHistory = new Repository<ApplicationSettingHistory>(Context);
        private static readonly IRepository<Action> RepositoryAction = new Repository<Action>(Context);

        private static readonly LanguageInstaller InstallerLanguage=new LanguageInstaller(RepositoryLanguage);
        private static readonly PersonInstaller InstallerPerson=new PersonInstaller(RepositoryPerson, RepositoryPersonHistory);
        private static readonly UserInstaller InstallerUser=new UserInstaller(RepositoryUser,RepositoryUserHistory);
        private static readonly RoleInstaller InstallerRole=new RoleInstaller(RepositoryUser,RepositoryRole,RepositoryRoleHistory);
        private static readonly RoleUserLineInstaller InstallerRoleUserLine=new RoleUserLineInstaller(RepositoryUser,RepositoryRoleUserLine,RepositoryRoleUserLineHistory);
        private static readonly ActionInstaller InstallerAction=new ActionInstaller(RepositoryAction);
        private static readonly ApplicationSettingInstaller InstallerApplicationSetting=new ApplicationSettingInstaller(RepositoryApplicationSetting);
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

                Step2Next();
                var counter = 1;
                var totalCount = InstallerLanguage.GetList().Count;
                foreach (var language in InstallerLanguage.GetList())
                {
                    InstallerLanguage.Add(language);
                    Console.WriteLine(@"Language {0}/{1} {2}", counter, totalCount, language.LanguageCode);
                    counter++;
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
                    
                    var counter = 1;
                    var totalCount = 1;
                    var addedPerson = InstallerPerson.Add(new Person
                    {
                        Id = Guid.NewGuid(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = DateTime.Now.AddYears(-35),
                        TcKimlikNo = "12345678901",
                        CreateDate = DateTime.Now,
                        IsApproved = true,
                        DisplayOrder = 1
                    });
                    Console.WriteLine(@"Person {0}/{1} {2}", counter, totalCount, addedPerson.Id);

                    InstallerUser.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Username = model.Username,
                        Password = model.Password,
                        Email = model.Email,
                        CreateDate = DateTime.Now,
                        IsApproved = true,
                        DisplayOrder = 1,
                        Language = InstallerLanguage.GetFirst(),
                        Person = InstallerPerson.GetFirst()
                    });

                    Console.WriteLine(@"User {0}/{1} {2}", counter, totalCount, model.Username);
                    var user = InstallerUser.GetFirst();


                    InstallerPerson.Add(new PersonHistory
                    {
                        Id = Guid.NewGuid(),
                        PersonId = addedPerson.Id,
                        FirstName = addedPerson.FirstName,
                        LastName = addedPerson.LastName,
                        DisplayOrder = addedPerson.DisplayOrder,
                        IsApproved = addedPerson.IsApproved,
                        CreateDate = DateTime.Now,
                        CreatedBy = user.Id,
                        VersionNo = 1,
                        RestoreVersionNo = 0,
                        IsDeleted = false
                    });

                    Console.WriteLine(@"PersonHistory {0}/{1} {2}", counter, totalCount, model.Username);

                    InstallerUser.Add(new UserHistory
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        PersonId = user.Person.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        LanguageId = user.Language.Id,
                        DisplayOrder = user.DisplayOrder,
                        IsApproved = user.IsApproved,
                        CreateDate = DateTime.Now,
                        CreatedBy = user.Id,
                        VersionNo = 1,
                        RestoreVersionNo = 0,
                        IsDeleted = false
                    });
                    Console.WriteLine(@"UserHistory {0}/{1} {2}", counter, totalCount, model.Username);

                    counter = 1;
                    totalCount = InstallerRole.GetList().Count;

                    foreach (var role in InstallerRole.GetList())
                    {
                        InstallerRole.Add(role);
                        Console.WriteLine(@"Role {0}/{1} {2}", counter, totalCount, role.RoleCode);
                        counter++;
                    }
                    counter = 1;
                    totalCount = InstallerRole.GetAll().Count();
                    foreach (var role in InstallerRole.GetAll())
                    {
                        var newGuid = Guid.NewGuid();
                        InstallerRole.Add(new RoleHistory
                        {
                            Id = newGuid,
                            RoleId = role.Id,
                            RoleCode = role.RoleCode,
                            DisplayOrder = role.DisplayOrder,
                            IsApproved = role.IsApproved,
                            CreateDate = DateTime.Now,
                            CreatedBy = user.Id,
                            VersionNo = 1,
                            RestoreVersionNo = 0,
                            IsDeleted = false
                        });
                        Console.WriteLine(@"RoleHistory {0}/{1} {2}", counter, totalCount, newGuid);
                    }

                    counter = 1;
                    totalCount = InstallerRoleUserLine.GetList(InstallerRole.GetAll()).Count;
                    foreach (var roleUserLine in InstallerRoleUserLine.GetList(InstallerRole.GetAll()))
                    {
                        InstallerRoleUserLine.Add(roleUserLine);
                        Console.WriteLine(@"RoleUserLine {0}/{1} {2}", counter, totalCount, roleUserLine.Id);
                        counter++;
                    }

                    counter = 1;
                    totalCount = InstallerRoleUserLine.GetAl().Count();
                    foreach (var roleUserLine in InstallerRoleUserLine.GetAl())
                    {
                        var newGuid = Guid.NewGuid();
                        InstallerRoleUserLine.Add(new RoleUserLineHistory
                        {
                            Id = newGuid,
                            RoleUserLineId = roleUserLine.Id,
                            RoleId = roleUserLine.Role.Id,
                            UserId = roleUserLine.User.Id,
                            CreateDate = DateTime.Now,
                            CreatedBy = user.Id,
                            VersionNo = 1,
                            RestoreVersionNo = 0,
                            IsDeleted = false
                        });
                        Console.WriteLine(@"RoleUserLineHistory {0}/{1} {2}", counter, totalCount, newGuid);
                        counter++;
                    }

                    counter = 1;
                    totalCount = InstallerAction.GetAll().Count();

                    foreach (var addedAction in InstallerAction.GetList().Select(action => InstallerAction.Add(action)))
                    {
                        Console.WriteLine(@"Action {0}/{1} {2}", counter, totalCount, addedAction.Id);
                        counter++;
                    }

                    counter = 1;
                    totalCount = InstallerApplicationSetting.GetList().Count;
                    foreach (var addedApplicationSetting in InstallerApplicationSetting.GetList().Select(applicationSetting => InstallerApplicationSetting.Add(applicationSetting)))
                    {
                        Console.WriteLine(@"RoleUserLineHistory {0}/{1} {2}", counter, totalCount, addedApplicationSetting.Id);
                        counter++;
                    }

                    Step4();

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
