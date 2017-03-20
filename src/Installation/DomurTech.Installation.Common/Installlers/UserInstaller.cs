using System;
using System.Linq;
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

        public void Set(AdminModel model)
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
                CreatedBy = user,
                UpdatedBy = user
            }; 
            var addedUser = _repositoryUser.Add(user);

            _repositoryUser.SaveChanges();

          //  person.CreatedBy = user;
           // person.UpdatedBy = user;

            //var addedPerson = _repositoryPerson.Add(person);
            //_repositoryPerson.SaveChanges();

           
            

           

            //_repositoryPersonHistory.Add(new PersonHistory
            //{
            //    Id = Guid.NewGuid(),
            //    PersonId = addedPerson.Id,
            //    FirstName = addedPerson.FirstName,
            //    LastName = addedPerson.LastName,
            //    DisplayOrder = addedPerson.DisplayOrder,
            //    IsApproved = addedPerson.IsApproved,
            //    CreateDate = addedPerson.CreateDate,
            //    CreatedBy = addedPerson.CreatedBy.Id,
            //    VersionNo = 1,
            //    RestoreVersionNo = 0,
            //    IsDeleted = false
            //});


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
            _repositoryUserHistory.SaveChanges();
        }
    }
}