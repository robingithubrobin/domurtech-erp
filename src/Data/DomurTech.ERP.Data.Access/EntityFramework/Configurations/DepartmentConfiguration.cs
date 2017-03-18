using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {

        public DepartmentConfiguration() : this("dbo")
        {

        }

        public DepartmentConfiguration(string schema)
        {
            ToTable("Departments", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.DepartmentCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_DepartmentCode", 1) { IsUnique = true }));
            Property(x => x.DepartmentName).IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.DepartmentsCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.DepartmentsUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany(y => y.Departments).WillCascadeOnDelete(false);
        }
    }
}