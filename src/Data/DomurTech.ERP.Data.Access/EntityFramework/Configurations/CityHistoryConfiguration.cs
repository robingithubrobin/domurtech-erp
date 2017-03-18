using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CityHistoryConfiguration : EntityTypeConfiguration<CityHistory>
    {

        public CityHistoryConfiguration() : this("dbo")
        {

        }

        public CityHistoryConfiguration(string schema)
        {
            ToTable("CityHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.CityId).IsRequired();
            Property(x => x.CityCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CountryId).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
        }
    }
}