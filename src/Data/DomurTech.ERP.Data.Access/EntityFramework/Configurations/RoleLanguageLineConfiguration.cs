using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class RoleLanguageLineConfiguration : EntityTypeConfiguration<RoleLanguageLine>
    {

        public RoleLanguageLineConfiguration() : this("dbo")
        {
            
        }

        public RoleLanguageLineConfiguration(string schema)
        {
            ToTable("RoleLanguageLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.RoleName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.RoleDescription).IsOptional().HasColumnType("nvarchar").HasMaxLength(4000);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.RoleLanguageLinesCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.RoleLanguageLinesUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.Role).WithMany(y => y.RoleLanguageLines).WillCascadeOnDelete(false);
            HasRequired(x => x.Language).WithMany(y => y.RoleLanguageLines).WillCascadeOnDelete(false);
        }
    }

}
