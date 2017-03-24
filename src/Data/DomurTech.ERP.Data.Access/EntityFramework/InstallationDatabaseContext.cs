using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;

namespace DomurTech.ERP.Data.Access.EntityFramework
{
    public class InstallationDatabaseContext : DbContext, IDatabaseContext
    {
        public IDbSet<T> Set<T>() where T : class => base.Set<T>();

        public InstallationDatabaseContext()
        {
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new DropCreateDatabaseAlways<InstallationDatabaseContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            var entityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type => !string.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var configurationInstance in entityTypeConfigurations.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
