using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class Country : IEntity
    {
        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual IList<CountryLanguageLine> CountryLanguageLines { get; set; }
        public virtual IList<City> Cities { get; set; }
    }
}
