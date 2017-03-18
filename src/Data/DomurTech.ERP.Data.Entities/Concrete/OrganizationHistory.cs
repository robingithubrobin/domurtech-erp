using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class OrganizationHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public Guid DistrictId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}
