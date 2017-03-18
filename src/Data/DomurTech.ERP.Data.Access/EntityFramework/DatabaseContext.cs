using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;

namespace DomurTech.ERP.Data.Access.EntityFramework
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public DatabaseContext()
        {
            Configuration.LazyLoadingEnabled = false;
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
