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
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.PersonsCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.PersonsUpdatedBy).WillCascadeOnDelete(false);
        }
    }
}