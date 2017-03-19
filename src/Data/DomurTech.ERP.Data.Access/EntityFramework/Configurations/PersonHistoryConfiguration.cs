using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class PersonHistoryConfiguration : EntityTypeConfiguration<PersonHistory>
    {

        public PersonHistoryConfiguration() : this("dbo")
        {

        }

        public PersonHistoryConfiguration(string schema)
        {
            ToTable("PersonHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
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