using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class OrganizationHistoryConfiguration : EntityTypeConfiguration<OrganizationHistory>
    {

        public OrganizationHistoryConfiguration() : this("dbo")
        {

        }

        public OrganizationHistoryConfiguration(string schema)
        {
            ToTable("OrganizationHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.OrganizationId).IsRequired();
            Property(x => x.OrganizationCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.OrganizationName).IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.TaxOffice).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.TaxNumber).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Address).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Phone).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Fax).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
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