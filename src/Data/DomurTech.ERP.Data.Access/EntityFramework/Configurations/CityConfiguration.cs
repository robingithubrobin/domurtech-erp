using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CityConfiguration : EntityTypeConfiguration<City>
    {

        public CityConfiguration() : this("dbo")
        {

        }

        public CityConfiguration(string schema)
        {
            ToTable("Cities", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.CityCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_CityCode", 1) { IsUnique = true }));
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.CitiesCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.CitiesUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.Country).WithMany(y => y.Cities).WillCascadeOnDelete(false);
        }
    }
}