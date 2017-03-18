using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using DomurTech.Providers.DataAccess.EntityFramework.Abstract;

namespace DomurTech.Providers.DataAccess.EntityFramework
{
    internal class DatabaseContext : DbContext, IDatabaseContext
    {
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type => !string.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var configurationInstance in entityTypeConfigurations.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
