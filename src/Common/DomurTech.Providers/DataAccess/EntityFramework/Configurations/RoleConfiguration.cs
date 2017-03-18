using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers.DataAccess.EntityFramework.Configurations
{

    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {

        public RoleConfiguration() : this("dbo")
        {

        }

        public RoleConfiguration(string schema)
        {
            ToTable("Roles", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.RoleCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_RoleCode", 1) { IsUnique = true }));
        }
    }
}