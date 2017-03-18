using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class RoleLanguageLineHistoryConfiguration : EntityTypeConfiguration<RoleLanguageLineHistory>
    {

        public RoleLanguageLineHistoryConfiguration() : this("dbo")
        {
            
        }

        public RoleLanguageLineHistoryConfiguration(string schema)
        {
            ToTable("RoleLanguageLineHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.RoleLanguageLineId).IsRequired();
            Property(x => x.RoleId).IsRequired();
            Property(x => x.LanguageId).IsRequired();

            Property(x => x.RoleName).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.RoleDescription).IsOptional().HasColumnType("nvarchar").HasMaxLength(4000);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

        }
    }

}
