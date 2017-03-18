using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class UserHistoryConfiguration : EntityTypeConfiguration<UserHistory>
    {

        public UserHistoryConfiguration() : this("dbo")
        {

        }

        public UserHistoryConfiguration(string schema)
        {
            ToTable("UserHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Username).IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Password).IsRequired().HasColumnType("char").HasMaxLength(128);
            Property(x => x.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LanguageId).IsRequired();
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