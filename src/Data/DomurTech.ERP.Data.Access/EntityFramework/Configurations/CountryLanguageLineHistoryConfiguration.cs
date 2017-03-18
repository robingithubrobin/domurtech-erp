using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CountryLanguageLineHistoryConfiguration : EntityTypeConfiguration<CountryLanguageLineHistory>
    {

        public CountryLanguageLineHistoryConfiguration() : this("dbo")
        {
            
        }

        public CountryLanguageLineHistoryConfiguration(string schema)
        {
            ToTable("CountryLanguageLineHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CountryLanguageLineId).IsRequired();
            Property(x => x.CountryId).IsRequired();
            Property(x => x.LanguageId).IsRequired();

            Property(x => x.CountryName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

        }
    }

}
