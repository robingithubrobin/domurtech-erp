using System;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Entities
{
    
    internal class Setting : IEntity
    {
        public Guid Id { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool Erasable { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Checked { get; set; }

    }
}
