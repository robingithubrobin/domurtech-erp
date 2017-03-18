using System.Data.Entity.ModelConfiguration;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Data.Access.EntityFramework.Configurations
{

    internal class ActionConfiguration : EntityTypeConfiguration<Action>
    {

        public ActionConfiguration() : this("dbo")
        {

        }

        public ActionConfiguration(string schema)
        {
            ToTable("Actions", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();

            Property(x => x.ControllerName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.ActionName).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
           

        }
    }

}
