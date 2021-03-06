using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers.DataAccess.EntityFramework.Configurations
{

    internal class LanguageConfiguration : EntityTypeConfiguration<Language>
    {

        public LanguageConfiguration() : this("dbo")
        {

        }

        public LanguageConfiguration(string schema)
        {
            ToTable("Languages", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.LanguageCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(10).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_LanguageCode", 1) { IsUnique = true }));
            Property(x => x.LanguageName).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();
            Ignore(x => x.Checked);
        }
    }
}