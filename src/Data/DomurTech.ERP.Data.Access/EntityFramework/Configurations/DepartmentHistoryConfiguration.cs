using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DepartmentHistoryConfiguration : EntityTypeConfiguration<DepartmentHistory>
    {

        public DepartmentHistoryConfiguration() : this("dbo")
        {

        }

        public DepartmentHistoryConfiguration(string schema)
        {
            ToTable("DepartmentHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.DepartmentId).IsRequired();
            Property(x => x.DepartmentCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.DepartmentName).IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.OrganizationId).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
        }
    }
}