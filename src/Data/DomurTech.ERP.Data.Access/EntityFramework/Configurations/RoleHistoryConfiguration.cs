using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class RoleHistoryConfiguration : EntityTypeConfiguration<RoleHistory>
    {

        public RoleHistoryConfiguration() : this("dbo")
        {

        }

        public RoleHistoryConfiguration(string schema)
        {
            ToTable("RoleHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.RoleId).IsRequired();
            Property(x => x.RoleCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
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