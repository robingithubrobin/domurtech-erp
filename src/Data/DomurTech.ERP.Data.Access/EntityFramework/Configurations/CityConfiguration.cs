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

            Property(x => x.CityCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_CityCode") { IsUnique = true }));
            Property(x => x.DisplayOrder).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            HasRequired(x => x.Country).WithMany(y => y.Cities).WillCascadeOnDelete(false);
        }
    }
}