using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CountryLanguageLineConfiguration : EntityTypeConfiguration<CountryLanguageLine>
    {

        public CountryLanguageLineConfiguration() : this("dbo")
        {
            
        }

        public CountryLanguageLineConfiguration(string schema)
        {
            ToTable("CountryLanguageLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CountryName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            HasRequired(x => x.Country).WithMany(y => y.CountryLanguageLines).WillCascadeOnDelete(false);
            HasRequired(x => x.Language).WithMany(y => y.CountryLanguageLines).WillCascadeOnDelete(false);
        }
    }

}
