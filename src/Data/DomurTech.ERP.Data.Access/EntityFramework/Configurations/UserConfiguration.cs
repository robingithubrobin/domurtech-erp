using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class UserConfiguration : EntityTypeConfiguration<User>
    {

        public UserConfiguration() : this("dbo")
        {

        }

        public UserConfiguration(string schema)
        {
            ToTable("Users", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Username).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_Username", 1) { IsUnique = true }));
            Property(x => x.Password).IsRequired().HasColumnType("char").HasMaxLength(128);
            Property(x => x.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
           
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.UsersCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.UsersUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.Language).WithMany(y => y.Users).WillCascadeOnDelete(false);
            Ignore(x => x.FullName);
            Ignore(x => x.DisplayName);
            Ignore(x => x.ConfirmPassword);
            Ignore(x => x.OldPassword);
        }
    }
}