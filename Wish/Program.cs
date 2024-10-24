using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WalkingTec.Mvvm.Core;
using log4net;
using System;

namespace Wish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return
                Host.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration((hostingContext, config) =>
                  {
                      config.AddInMemoryCollection(new Dictionary<string, string> { { "HostRoot", hostingContext.HostingEnvironment.ContentRootPath } });
                  })
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.ClearProviders();
                     logging.AddConsole();
                     logging.AddWTMLogger();
                 })
                 //.ConfigureLogging(logging =>
                 //{
                 //    logging.ClearProviders();
                 //    logging.AddWTMLogger();
                 //    logging.ClearProviders();
                 //    logging.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "log.config"));
                 //    logging.AddConsole();
                 //})
                 .ConfigureAppConfiguration(option =>
                 {
                     option.AddJsonFile("appsettings.json", true, false);
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseUrls("http://*:8260").UseStartup<Startup>();
                });
        }
    }
}
