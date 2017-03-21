using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Helpers;
using DomurTech.Installation.Common.Models;

namespace DomurTech.Installation.Common.Installlers
{
    public class UserInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<UserHistory> _repositoryUserHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<Person> _repositoryPerson;
        private readonly IRepository<PersonHistory> _repositoryPersonHistory;

        public UserInstaller(IRepository<User> repositoryUser, IRepository<Language> repositoryLanguage, IRepository<UserHistory> repositoryUserHistory, IRepository<Person> repositoryPerson, IRepository<PersonHistory> repositoryPersonHistory)
        {
            _repositoryUser = repositoryUser;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUserHistory = repositoryUserHistory;
            _repositoryPerson = repositoryPerson;
            _repositoryPersonHistory = repositoryPersonHistory;
        }

        public bool Exists()
        {
            return _repositoryUser.Get().Any();
        }

        public List<User> Set(AdminModel model)
        {
            var result = new List<User>();
            var thread = new Thread(() =>
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = model.Username,
                    Password = model.Password.ToSha512(),
                    Email = model.Email,
                    DisplayOrder = 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                user.CreatedBy = user;
                user.UpdatedBy = user;
                user.Language = _repositoryLanguage.Get().FirstOrDefault(x => x.DisplayOrder == 1);

                user.Person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TcKimlikNo = "12345678901",
                    BirthDate = DateTime.Now.AddYears(-35),
                    DisplayOrder = 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                var addedUser = _repositoryUser.Add(user);
                result.Add("User " + 1 + " / " + 1 + " " + model.Username);

                _repositoryUser.SaveChanges();

                _repositoryUserHistory.Add(new UserHistory
                {
                    Id = Guid.NewGuid(),
                    UserId = addedUser.Id,
                    Username = addedUser.Username,
                    Password = addedUser.Username,
                    Email = addedUser.Email,
                    LanguageId = addedUser.Language.Id,
                    PersonId = addedUser.Person.Id,
                    DisplayOrder = addedUser.DisplayOrder,
                    IsApproved = addedUser.IsApproved,
                    CreateDate = addedUser.CreateDate,
                    CreatedBy = addedUser.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false

                });
                result.Add("UserHistory " + 1 + " / " + 1 + " " + model.Username);
                _repositoryUserHistory.SaveChanges();
            });
            thread.Start();
            return result;
            
        }
    }
}