using System;

namespace DomurTech.ERP.Service.Entities.Concrete.RoleModels
{
    public class DetailModel
    {
        public Guid RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
