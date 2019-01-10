﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace IdentityServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//var seed = args.Any(x => x == "/seed");
			//if (seed) args = args.Except(new[] { "/seed" }).ToArray();

			var host = CreateWebHostBuilder(args).Build();

			//if (seed)
			//{
			//    var config = host.Services.GetRequiredService<IConfiguration>();
			//    var connectionString = config.GetConnectionString("DefaultConnection");
			//    SeedData.EnsureSeedData(connectionString);
			//    return;
			//}

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					services.GetRequiredService<DbInitializer>().Initialize();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseSerilog((context, configuration) =>
				{
					configuration
						.MinimumLevel.Debug()
						.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
						.MinimumLevel.Override("System", LogEventLevel.Warning)
						.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
						.Enrich.FromLogContext()
						.WriteTo.File(@"identityserver4_log.txt")
						.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
				});
		}
    }
}
