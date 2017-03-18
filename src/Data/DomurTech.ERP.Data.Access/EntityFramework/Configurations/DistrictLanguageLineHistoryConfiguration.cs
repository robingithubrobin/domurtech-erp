using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DistrictLanguageLineHistoryConfiguration : EntityTypeConfiguration<DistrictLanguageLineHistory>
    {

        public DistrictLanguageLineHistoryConfiguration() : this("dbo")
        {
            
        }

        public DistrictLanguageLineHistoryConfiguration(string schema)
        {
            ToTable("DistrictLanguageLineHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.DistrictLanguageLineId).IsRequired();
            Property(x => x.DistrictId).IsRequired();
            Property(x => x.LanguageId).IsRequired();

            Property(x => x.DistrictName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

        }
    }

}
