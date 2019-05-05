using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdentityDAL
{
    public class IdentityDataDbContextFactory : IDesignTimeDbContextFactory<IdentityDataDbContext>
    {
        public IdentityDataDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityDataDbContext>();
            builder.UseNpgsql(ConfigurationHelper.DefaultConnectionString, sql => sql.MigrationsAssembly(typeof(IdentityDataDbContextFactory).GetTypeInfo().Assembly.GetName().Name));
            return new IdentityDataDbContext(builder.Options);
        }
    }

    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseNpgsql(ConfigurationHelper.DefaultConnectionString, sql => sql.MigrationsAssembly(typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly.GetName().Name));
            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions());
        }
    }

    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            builder.UseNpgsql(ConfigurationHelper.DefaultConnectionString, sql => sql.MigrationsAssembly(typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly.GetName().Name));
            return new PersistedGrantDbContext(builder.Options, new OperationalStoreOptions());
        }
    }
}
