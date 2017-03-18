using System.Data.Entity.ModelConfiguration;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers.DataAccess.EntityFramework.Configurations
{

    internal class RoleActionLineConfiguration : EntityTypeConfiguration<RoleActionLine>
    {

        public RoleActionLineConfiguration() : this("dbo")
        {
            
        }

        public RoleActionLineConfiguration(string schema)
        {
            ToTable("RoleActionLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            HasRequired(x => x.Role).WithMany(y => y.RoleActionLines).WillCascadeOnDelete(false);
            HasRequired(x => x.Action).WithMany(y => y.RoleActionLines).WillCascadeOnDelete(false);
        }
    }

}
