using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {

        public OrganizationConfiguration() : this("dbo")
        {

        }

        public OrganizationConfiguration(string schema)
        {
            ToTable("Organizations", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.OrganizationCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_OrganizationCode") { IsUnique = true }));
            Property(x => x.OrganizationName).IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.TaxOffice).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.TaxNumber).IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Address).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Phone).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Fax).IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.DisplayOrder).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            HasRequired(x => x.District).WithMany(y => y.Organizations).WillCascadeOnDelete(false);
        }
    }
}