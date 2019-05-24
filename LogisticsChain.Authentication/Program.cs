using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LogisticsChain.AuthenticService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "AuthenticationAPI";
            //CreateWebHostBuilder(args).Build().Run(); 54961
            BuildWebHost(args).Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  var env = hostingContext.HostingEnvironment;
                  config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                  config.AddEnvironmentVariables();
              })
           .UseUrls("http://*:54961")
               .UseStartup<Startup>()
               .Build();
    }
}
