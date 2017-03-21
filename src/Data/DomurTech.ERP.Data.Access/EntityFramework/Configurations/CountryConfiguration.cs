using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class CountryConfiguration : EntityTypeConfiguration<Country>
    {

        public CountryConfiguration() : this("dbo")
        {

        }

        public CountryConfiguration(string schema)
        {
            ToTable("Countries", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.CountryCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_CountryCode", 1) { IsUnique = true }));
            Property(x => x.DisplayOrder).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
        }
    }
}