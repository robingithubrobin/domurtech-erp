using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.UI.Web.Installation.Models;
using DomurTech.Helpers;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.Installlers
{
    internal class UserInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<UserHistory> _repositoryUserHistory;
        private readonly IRepository<Language> _repositoryLanguage;

        public UserInstaller(IRepository<User> repositoryUser, IRepository<Language> repositoryLanguage, IRepository<UserHistory> repositoryUserHistory)
        {
            _repositoryUser = repositoryUser;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUserHistory = repositoryUserHistory;
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
                FirstName = model.FirstName,
                LastName = model.LastName,
                DisplayOrder = 1,
                IsApproved = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            user.CreatedBy = user;
            user.UpdatedBy = user;
            user.Language = _repositoryLanguage.Get().FirstOrDefault(x => x.DisplayOrder == 1);

            var addedUser = _repositoryUser.Add(user);
            _repositoryUser.SaveChanges();

            _repositoryUserHistory.Add(new UserHistory
            {
                Id = Guid.NewGuid(),
                UserId = addedUser.Id,
                Username = addedUser.Username,
                Password = addedUser.Username,
                Email = addedUser.Email,
                FirstName = addedUser.FirstName,
                LastName = addedUser.LastName,
                LanguageId = addedUser.Language.Id,
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