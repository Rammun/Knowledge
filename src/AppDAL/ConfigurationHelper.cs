using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppDAL
{
    public static class ConfigurationHelper
    {
        private static IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public static string DefaultConnectionString => ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
    }
}
