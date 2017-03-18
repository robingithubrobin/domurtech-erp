using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class DistrictHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public Guid CityId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}
