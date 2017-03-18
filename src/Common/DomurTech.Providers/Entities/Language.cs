using System;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Entities
{
    public class Language : IEntity
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public bool Checked { get; set; }

    }

}
