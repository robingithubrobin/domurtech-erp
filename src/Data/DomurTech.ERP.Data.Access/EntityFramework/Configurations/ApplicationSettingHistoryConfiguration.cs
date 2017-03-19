using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class ApplicationSettingHistoryConfiguration : EntityTypeConfiguration<ApplicationSettingHistory>
    {

        public ApplicationSettingHistoryConfiguration() : this("dbo")
        {

        }

        public ApplicationSettingHistoryConfiguration(string schema)
        {
            ToTable("ApplicationSettingHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.SettingId).IsRequired();
            Property(x => x.SettingKey).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.SettingValue).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Erasable).IsRequired();
            Property(x => x.DisplayOrder).IsRequired();
            Property(x => x.IsApproved).IsRequired();

            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
        }
    }
}