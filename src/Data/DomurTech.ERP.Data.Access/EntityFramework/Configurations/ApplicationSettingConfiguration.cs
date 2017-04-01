using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class ApplicationSettingConfiguration : EntityTypeConfiguration<ApplicationSetting>
    {

        public ApplicationSettingConfiguration() : this("dbo")
        {

        }

        public ApplicationSettingConfiguration(string schema)
        {
            ToTable("ApplicationSettings", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.SettingKey).IsRequired().HasColumnType("nvarchar").HasMaxLength(100).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_SettingKey") { IsUnique = true }));
            Property(x => x.SettingValue).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Erasable).IsRequired();
            Property(x => x.DisplayOrder).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
        }
    }
}