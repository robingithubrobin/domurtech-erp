using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class Department : IEntity
    {
        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
