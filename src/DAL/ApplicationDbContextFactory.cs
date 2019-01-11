using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    //            .AddJsonFile("appsettings.json")
    //            .Build();
    //        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));
    //        return new ApplicationDbContext(builder.Options);
    //    }
    //}
}
