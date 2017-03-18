using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class DistrictConfiguration : EntityTypeConfiguration<District>
    {

        public DistrictConfiguration() : this("dbo")
        {

        }

        public DistrictConfiguration(string schema)
        {
            ToTable("Districts", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.DistrictCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_DistrictCode", 1) { IsUnique = true }));
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.DistrictsCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.DistrictsUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.City).WithMany(y => y.Districts).WillCascadeOnDelete(false);
        }
    }
}