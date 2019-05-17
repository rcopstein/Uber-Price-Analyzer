using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = hostingContext.HostingEnvironment;

                    // Development-only configurations
                    if (environment.IsDevelopment())
                    {
                        config.AddUserSecrets<Startup>();
                    }

                    // Production-only configurations
                    if (environment.IsProduction())
                    {
                        config.AddJsonFile("/etc/rider/database.credentials",
                                           optional: false,
                                           reloadOnChange: true);

                        config.AddJsonFile("/etc/rider/uber.credentials",
                                           false,
                                           true);
                    }
                })
                .UseStartup<Startup>();
        }
    }
}
