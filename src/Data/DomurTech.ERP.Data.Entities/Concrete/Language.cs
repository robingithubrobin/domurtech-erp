using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class Language : IEntity
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public virtual IList<CityLanguageLine> CityLanguageLines { get; set; }
        public virtual IList<CountryLanguageLine> CountryLanguageLines { get; set; }
        public virtual IList<DistrictLanguageLine> DistrictLanguageLines { get; set; }
        public virtual IList<RoleLanguageLine> RoleLanguageLines { get; set; }
        public virtual IList<User> Users { get; set; }

    }

}
