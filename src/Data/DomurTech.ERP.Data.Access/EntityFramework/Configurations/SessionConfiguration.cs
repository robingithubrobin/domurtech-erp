using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class SessionConfiguration : EntityTypeConfiguration<Session>
    {

        public SessionConfiguration() : this("dbo")
        {

        }

        public SessionConfiguration(string schema)
        {
            ToTable("Sessions", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.SessionsCreatedBy).WillCascadeOnDelete(false);
        }
    }
}