using System;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Service.Common.Abstract;
using DomurTech.ERP.Service.Entities.Concrete.AccountModels;

namespace DomurTech.ERP.Service.Common.Services
{
    public class AccountService : IAccountService
    {
        //private bool _disposed;
        //private readonly IAccountManager _accountManager;

        //public AccountService(IAccountManager accountManager)
        //{
        //    _accountManager = accountManager;
        //}

        //public AccountModel MyAccount()
        //{
        //    var result = new AccountModel();
        //    var account = _accountManager.MyAccount();
        //    result.FirstName = account.User.Person.FirstName;
        //    result.LastName = account.User.Person.LastName;
        //    result.Username = account.User.Username;
        //    result.LastLoginTime = account.LastLoginTime;
        //    result.RemainingSessionTime = account.RemainingSessionTime;
        //    return result;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //private void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _accountManager.Dispose();
        //        }
        //    }
        //    _disposed = true;
        //}
    }
}
