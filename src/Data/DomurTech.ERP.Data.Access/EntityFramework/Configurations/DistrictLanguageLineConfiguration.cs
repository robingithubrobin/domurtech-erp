using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DistrictLanguageLineConfiguration : EntityTypeConfiguration<DistrictLanguageLine>
    {

        public DistrictLanguageLineConfiguration() : this("dbo")
        {
            
        }

        public DistrictLanguageLineConfiguration(string schema)
        {
            ToTable("DistrictLanguageLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.DistrictName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            HasRequired(x => x.District).WithMany(y => y.DistrictLanguageLines).WillCascadeOnDelete(false);
            HasRequired(x => x.Language).WithMany(y => y.DistrictLanguageLines).WillCascadeOnDelete(false);
        }
    }

}
