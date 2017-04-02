using DomurTech.ERP.Service.Entities.Abstract;

namespace DomurTech.ERP.Service.Entities.Concrete.AccountModels
{
    public class AccountModel : IServiceEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string LastLoginTime { get; set; }
        public string RemainingSessionTime { get; set; }
        public string Message { get; set; }

    }
}
