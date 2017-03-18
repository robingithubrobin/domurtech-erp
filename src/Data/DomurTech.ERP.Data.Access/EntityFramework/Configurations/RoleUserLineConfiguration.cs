using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class RoleUserLineConfiguration : EntityTypeConfiguration<RoleUserLine>
    {

        public RoleUserLineConfiguration() : this("dbo")
        {
            
        }

        public RoleUserLineConfiguration(string schema)
        {
            ToTable("RoleUserLines", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.UpdateDate).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany(y => y.RoleUserLinesCreatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.UpdatedBy).WithMany(y => y.RoleUserLinesUpdatedBy).WillCascadeOnDelete(false);
            HasRequired(x => x.Role).WithMany(y => y.RoleUserLines).WillCascadeOnDelete(false);
            HasRequired(x => x.User).WithMany(y => y.RoleUserLines).WillCascadeOnDelete(false);
        }
    }

}
