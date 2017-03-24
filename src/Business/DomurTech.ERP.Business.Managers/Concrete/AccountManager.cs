using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using DomurTech.Core.Security;
using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Business.Managers.Options;
using DomurTech.ERP.Business.ValidationRules.FluentValidation.AccountRules;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Helpers;
using DomurTech.Providers;
using DomurTech.Validation.FluentValidation;

namespace DomurTech.ERP.Business.Managers.Concrete
{
    public class AccountManager : IAccountManager
    {
        private bool _disposed;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<UserHistory> _repositoryUserHistory;
        private readonly IRepository<RoleLanguageLine> _repositoryRoleLanguageLine;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<RoleUserLine> _repositoryRoleUserLine;
        private readonly IRepository<RoleUserLineHistory> _repositoryRoleUserLineHistory;
        private readonly IRepository<Session> _repositorySession;
        private readonly IRepository<SessionHistory> _repositorySessionHistory;

        public AccountManager(IRepository<User> repositoryUser, IRepository<UserHistory> repositoryUserHistory, IRepository<RoleLanguageLine> repositoryRoleLanguageLine, IRepository<Language> repositoryLanguage, IRepository<RoleUserLine> repositoryRoleUserLine, IRepository<RoleUserLineHistory> repositoryRoleUserLineHistory, IRepository<Session> repositorySession, IRepository<SessionHistory> repositorySessionHistory)
        {
            _repositoryUser = repositoryUser;
            _repositoryUserHistory = repositoryUserHistory;
            _repositoryRoleLanguageLine = repositoryRoleLanguageLine;
            _repositoryLanguage = repositoryLanguage;
            _repositoryRoleUserLine = repositoryRoleUserLine;
            _repositoryRoleUserLineHistory = repositoryRoleUserLineHistory;
            _repositorySession = repositorySession;
            _repositorySessionHistory = repositorySessionHistory;
        }

        public AccountModel MyAccount()
        {
            var identity = (Identity)Thread.CurrentPrincipal.Identity;
            var user = _repositoryUser.Get().Include(x=>x.SessionHistoriesCreatedBy).FirstOrDefault(e => e.Id == identity.UserId);
            if (user == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            var sessionHistories = user.SessionHistoriesCreatedBy;
            var remainingSessionTime = SystemSettings.SessionTimeOut;
            var lastLoginTime = DateTime.Now.ToString("G");
            if (sessionHistories.Count <= 0)
            {
                return new AccountModel
                {
                    User = user,
                    RemainingSessionTime = remainingSessionTime.ToString(),
                    LastLoginTime = lastLoginTime
                };
            }
            var lastSession = sessionHistories.OrderByDescending(e => e.UpdateDate).FirstOrDefault();
            if (lastSession != null)
            {
                lastLoginTime = lastSession.UpdateDate.ToString("G");
            }
            return new AccountModel
            {
                User = user,
                RemainingSessionTime = remainingSessionTime.ToString(),
                LastLoginTime = lastLoginTime
            };
        }

        public ForgotPasswordModel ForgotPassword()
        {
            return new ForgotPasswordModel
            {
                User = new User(),
                Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList()

            };
        }

        public void ForgotPassword(ForgotPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public SignUpModel SignUp()
        {
            return new SignUpModel
            {
                User = new User(),
                Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList()
            };
        }

        public void SignUp(SignUpModel model)
        {
            throw new NotImplementedException();
        }

        public UpdatePasswordModel UpdateMyPassword()
        {
            throw new NotImplementedException();
        }

        public void UpdateMyPassword(UpdatePasswordModel model)
        {
            throw new NotImplementedException();
        }

        public UpdateInformationModel UpdateMyInformation()
        {
            throw new NotImplementedException();
        }

        public void UpdateMyInformation(UpdateInformationModel model)
        {
            throw new NotImplementedException();
        }

        public LoginModel Login()
        {
            var loginModel = new LoginModel
            {
                User = new User(),
                Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList()

            };
            var identity = (Identity)Thread.CurrentPrincipal.Identity;
            if (!identity.RememberMe)
            {
                return loginModel;
            }
            loginModel.Username = identity.Username;
            loginModel.Password = identity.Password;
            loginModel.RememberMe = identity.RememberMe;
            return loginModel;
        }

        public void Login(LoginModel model)
        {
            model.Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList();

            var validator = new FluentBaseValidator<LoginModel, LoginRules>(model);
            model.User = new User
            {
                Username = model.Username,
                Password = model.Password
            };
            var validationResults = validator.Validate();
            if (!validator.IsValid)
            {
                throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                {
                    ValidationResult = validationResults
                };
            }
            var user = _repositoryUser.Get()
                .Include(x=>x.SessionsCreatedBy)
                .Include(x=>x.RoleUserLines.Select(t=>t.Role))
                .Include(x=>x.Language)
                .FirstOrDefault(e => e.Username == model.Username);

            // kullanıcı sistemde yok
            if (user == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }

            if (model.Password.ToSha512() != user.Password)
            {
                throw new NotFoundException(Messages.DangerIncorrectPassword);
            }

            if (!user.IsApproved)
            {
                throw new NotApprovedException(Messages.DangerItemNotApproved);
            }
            var sessionIdList = new List<Guid>();
            if (user.SessionsCreatedBy.Count > 0)
            {
                foreach (var session in user.SessionsCreatedBy)
                {
                    _repositorySessionHistory.Add(new SessionHistory
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = session.CreatedBy,
                        CreateDate = session.CreateDate,
                        UpdateDate = DateTime.Now,
                        LogoutType = LogoutOptions.InvalidLogout.ToString()
                    });

                    _repositorySessionHistory.SaveChanges();

                    sessionIdList.Add(session.Id);
                }
            }
            foreach (var i in sessionIdList)
            {
                _repositorySession.Delete(_repositorySession.Get().FirstOrDefault(e => e.Id == i));
                _repositorySession.SaveChanges();
            }
            _repositorySession.Add(new Session
            {
                Id = Guid.NewGuid(),
                CreatedBy = user,
                CreateDate = DateTime.Now
            });

            _repositorySession.SaveChanges();

            if (user.RoleUserLines.Count <= 0)
            {
                throw new NotApprovedException(Messages.DangerItemNotApproved);
            }
            
            var identity = new Identity
            {
                UserId = user.Id,
                Username = user.Username,
                Password = !model.RememberMe ? null : model.Password,
                IsAuthenticated = true,
                RememberMe = model.RememberMe,
                LanguageCode = user.Language.LanguageCode,
                Name = user.FullName,
                Roles = user.RoleUserLines.Select(t => t.Role).OrderBy(c => c.DisplayOrder).Select(x => x.RoleCode).ToList()
            };
            Thread.CurrentPrincipal = new Principal(identity);
        }

        public void Logout(LogoutOptions logoutOptions)
        {
            var identity = (Identity)Thread.CurrentPrincipal.Identity;
            var sessions = _repositorySession.Get().Include(x=>x.CreatedBy).Where(e => e.CreatedBy.Id == identity.UserId).ToList();
            if (sessions.Count > 0)
            {
                foreach (var session in sessions)
                {
                    _repositorySessionHistory.Add(new SessionHistory
                    {
                        CreatedBy = session.CreatedBy,
                        CreateDate = session.CreateDate,
                        UpdateDate = DateTime.Now,
                        LogoutType = logoutOptions.ToString()
                    });

                    _repositorySessionHistory.SaveChanges();

                    _repositorySession.Delete(session);
                    _repositorySession.SaveChanges();
                }
            }
            identity.UserId = Guid.Empty;
            identity.IsAuthenticated = false;
            identity.Name = "Guest";
            identity.Username = "Guest";
            identity.LanguageCode = Thread.CurrentThread.CurrentCulture.ToString();
            identity.RememberMe = false;
            identity.Roles = new List<string>();
            Thread.CurrentPrincipal = new Principal(identity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositoryUser.Dispose();
                    _repositoryUserHistory.Dispose();
                    _repositoryRoleLanguageLine.Dispose();
                    _repositoryLanguage.Dispose();
                    _repositoryRoleUserLine.Dispose();
                    _repositoryRoleUserLineHistory.Dispose();
                    _repositorySession.Dispose();
                    _repositorySessionHistory.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
