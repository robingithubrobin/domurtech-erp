using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class CityLanguageLineHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid CityLanguageLineId { get; set; }
        public Guid CityId { get; set; }
        public Guid LanguageId { get; set; }
        public string CityName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}
