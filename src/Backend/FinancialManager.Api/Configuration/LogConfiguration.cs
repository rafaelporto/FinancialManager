using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace FinancialManager.Api.Configuration
{
    public static class LogConfiguration
    {
        public static ILogger BuildLogger()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();;
            
            return Log.Logger;
        }
    }
}