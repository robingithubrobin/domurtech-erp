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

        public UserInstaller(IRepository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public bool Exists()
        {
            return _repositoryUser.Get().Any();
        }

        public User Add(User item)
        {
            var result = _repositoryUser.Add(item);
            _repositoryUser.SaveChanges();
            return result;

        }
    }
}