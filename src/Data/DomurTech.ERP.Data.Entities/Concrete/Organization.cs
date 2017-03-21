using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class Organization : IEntity
    {
        public Guid Id { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual District District { get; set; }
        public virtual IList<Department> Departments { get; set; }
    }
}
