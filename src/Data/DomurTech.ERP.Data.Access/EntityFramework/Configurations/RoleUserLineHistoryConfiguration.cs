using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class RoleUserLineHistoryConfiguration : EntityTypeConfiguration<RoleUserLineHistory>
    {

        public RoleUserLineHistoryConfiguration() : this("dbo")
        {
            
        }

        public RoleUserLineHistoryConfiguration(string schema)
        {
            ToTable("RoleUserLineHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.RoleUserLineId).IsRequired();
            Property(x => x.RoleId).IsRequired();
            Property(x => x.UserId).IsRequired();

            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.VersionNo).IsRequired();
            Property(x => x.RestoreVersionNo).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

        }
    }

}
