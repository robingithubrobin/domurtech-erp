using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class City : IEntity
    {
        public Guid Id { get; set; }
        public string CityCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Country Country { get; set; }
        public virtual IList<CityLanguageLine> CityLanguageLines { get; set; }
        public virtual IList<District> Districts { get; set; }
    }
}
