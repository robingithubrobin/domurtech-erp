using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class ApplicationSetting : IEntity
    {
        public Guid Id { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool Erasable { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }

    }

}
