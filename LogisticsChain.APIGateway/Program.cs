using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LogisticsChain.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
          //  CreateWebHostBuilder(args).Build().Run();
            BuildWebHost(args).Run();
        }
        static string IP= "localhost";
        static string Port= "58547";
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();





        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                            .UseUrls($"http://{IP}:{Port}")
                            .ConfigureAppConfiguration((hostingContext, builder) =>
                            {
                                builder.AddJsonFile("configuration.json", false, true);
                            })
                            .Build();
        }


    }
}
