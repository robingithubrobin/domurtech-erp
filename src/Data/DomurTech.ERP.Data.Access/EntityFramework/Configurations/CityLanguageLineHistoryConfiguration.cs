using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CityLanguageLineHistoryConfiguration : EntityTypeConfiguration<CityLanguageLineHistory>
    {

        public CityLanguageLineHistoryConfiguration() : this("dbo")
        {
            
        }

        public CityLanguageLineHistoryConfiguration(string schema)
        {
            ToTable("CityLanguageLineHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CityLanguageLineId).IsRequired();
            Property(x => x.CityId).IsRequired();
            Property(x => x.LanguageId).IsRequired();

            Property(x => x.CityName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

        }
    }

}
