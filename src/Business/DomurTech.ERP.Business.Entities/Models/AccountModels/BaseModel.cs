using DomurTech.Core.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class BaseModel : IBaseModel
    {
        public User User { get; set; }
        public string Message { get; set; }

    }
}
