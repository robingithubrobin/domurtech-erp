using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers.DataAccess.EntityFramework.Configurations
{

    internal class SettingConfiguration : EntityTypeConfiguration<Setting>
    {

        public SettingConfiguration() : this("dbo")
        {

        }

        public SettingConfiguration(string schema)
        {
            ToTable("Settings", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.SettingKey).IsRequired().HasColumnType("nvarchar").HasMaxLength(100).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_SettingKey", 1) { IsUnique = true }));
            Property(x => x.SettingValue).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Erasable).IsRequired();
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            Ignore(x => x.Checked);



        }
    }
}