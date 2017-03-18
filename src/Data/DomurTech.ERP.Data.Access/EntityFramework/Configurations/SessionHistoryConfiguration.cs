using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class SessionHistoryConfiguration : EntityTypeConfiguration<SessionHistory>
    {

        public SessionHistoryConfiguration() : this("dbo")
        {

        }

        public SessionHistoryConfiguration(string schema)
        {
            ToTable("SessionHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.LogoutType).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
           
            HasRequired(x => x.CreatedBy).WithMany(y => y.SessionHistoriesCreatedBy).WillCascadeOnDelete(false);
        }
    }
}