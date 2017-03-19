using DomurTech.Core.Abstract;

namespace DomurTech.Installation.Common.Models
{
    public class DatabaseConnectionModel : IBaseModel
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
    }
}