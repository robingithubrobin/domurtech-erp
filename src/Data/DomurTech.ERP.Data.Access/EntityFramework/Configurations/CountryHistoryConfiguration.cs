using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CountryHistoryConfiguration : EntityTypeConfiguration<CountryHistory>
    {

        public CountryHistoryConfiguration() : this("dbo")
        {

        }

        public CountryHistoryConfiguration(string schema)
        {
            ToTable("CountryHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.CountryId).IsRequired();
            Property(x => x.CountryCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
        }
    }
}