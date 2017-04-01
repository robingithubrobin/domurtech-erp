using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class District : IEntity
    {
        public Guid Id { get; set; }
        public string DistrictCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<DistrictLanguageLine> DistrictLanguageLines { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
