namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class AccountModel : BaseModel
    {
        public string LastLoginTime { get; set; }
        public string RemainingSessionTime { get; set; }
    }
}
