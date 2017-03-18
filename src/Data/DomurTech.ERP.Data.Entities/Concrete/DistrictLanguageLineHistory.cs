using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class DistrictLanguageLineHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid DistrictLanguageLineId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid LanguageId { get; set; }
        public string DistrictName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}
