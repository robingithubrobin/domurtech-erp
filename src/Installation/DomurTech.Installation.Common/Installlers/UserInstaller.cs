using System.Data.Entity;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.Installlers
{
    public class UserInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<UserHistory> _repositoryUserHistory;

        public UserInstaller(IRepository<User> repositoryUser, IRepository<UserHistory> repositoryUserHistory)
        {
            _repositoryUser = repositoryUser;
            _repositoryUserHistory = repositoryUserHistory;
        }

        public User Add(User user)
        {
            var result = _repositoryUser.Add(user);
            _repositoryUser.SaveChanges();
            return result;
        }

        public User GetFirst()
        {
            return _repositoryUser.Get().Include(x=>x.Language).Include(x=>x.Person).FirstOrDefault(x=>x.DisplayOrder==1);
        }

        public void Add(UserHistory user)
        {
            _repositoryUserHistory.Add(user);
            _repositoryUserHistory.SaveChanges();
        }

        public bool Exists()
        {
            return _repositoryUser.Get().Any();
        }
    }
}