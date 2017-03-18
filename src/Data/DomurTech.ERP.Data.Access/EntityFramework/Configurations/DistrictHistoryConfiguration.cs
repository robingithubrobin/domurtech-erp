using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DistrictHistoryConfiguration : EntityTypeConfiguration<DistrictHistory>
    {

        public DistrictHistoryConfiguration() : this("dbo")
        {

        }

        public DistrictHistoryConfiguration(string schema)
        {
            ToTable("DistrictHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.DistrictId).IsRequired();
            Property(x => x.DistrictCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CityId).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
        }
    }
}