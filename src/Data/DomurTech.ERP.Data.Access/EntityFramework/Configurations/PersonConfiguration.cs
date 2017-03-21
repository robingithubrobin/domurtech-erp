using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class PersonConfiguration : EntityTypeConfiguration<Person>
    {

        public PersonConfiguration() : this("dbo")
        {

        }

        public PersonConfiguration(string schema)
        {
            ToTable("Persons", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.TcKimlikNo).IsOptional();
            Property(x => x.BirthDate).IsOptional();
            Property(x => x.DisplayOrder).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
        }
    }
}