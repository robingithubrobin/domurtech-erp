using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CityLanguageLineConfiguration : EntityTypeConfiguration<CityLanguageLine>
    {

        public CityLanguageLineConfiguration() : this("dbo")
        {
            
        }

        public CityLanguageLineConfiguration(string schema)
        {
            ToTable("CityLanguageLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CityName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.CityLanguageLinesCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.CityLanguageLinesUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.City).WithMany(y => y.CityLanguageLines).WillCascadeOnDelete(false);
            HasRequired(x => x.Language).WithMany(y => y.CityLanguageLines).WillCascadeOnDelete(false);
        }
    }

}
