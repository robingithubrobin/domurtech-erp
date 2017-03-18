using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.ERP.Business.Managers.Options;

namespace DomurTech.ERP.Business.Managers.Abstract
{
    public interface IAccountManager
    {
        AccountModel MyAccount();
        ForgotPasswordModel ForgotPassword();
        void ForgotPassword(ForgotPasswordModel model);
        SignUpModel SignUp();
        void SignUp(SignUpModel model);
        UpdatePasswordModel UpdateMyPassword();
        void UpdateMyPassword(UpdatePasswordModel model);
        UpdateInformationModel UpdateMyInformation();
        void UpdateMyInformation(UpdateInformationModel model);
        LoginModel Login();
        void Login(LoginModel model);
        void Logout(LogoutOptions logoutOptions);


    }
}
