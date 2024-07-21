using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FamilySync.Core;

public static class ServiceHost<TEntryPoint> where TEntryPoint : EntryPoint, new()
{
    public static int Run(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var loggerConfig = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Service", config["Config:Service:Name"]);

        var isDevelopment = bool.Parse(config["Config:Service:IsDevelopment"]!);

        if (isDevelopment)
        {
            Log.Logger = loggerConfig
                .WriteTo.Console()
                .CreateLogger();
        }
        else
        {
            // TODO: Research and implement a log management and analytics platform such as Graylog
        }
        
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            
            var entryPoint = new TEntryPoint
            {
                Configuration = builder.Configuration
            };
            
            entryPoint.ConfigureServices(builder.Services);

            var app = builder.Build();
            
            entryPoint.ConfigureApp(app);
            
            // Handle arguments making it possible to execute custom logic

            switch (args.Any() ? args[0] : string.Empty)
            {
                case "migrate":
                {
                    // TODO: Implement cloud based database migration
                    break;
                }

                default:
                {
                    app.Run();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"A fatal error occured while executing host! {ex}");
        }

        return 0;
    }
}

public static class ServiceHost
{
    public static int Run(string[] args)
    {
        return ServiceHost<EntryPoint>.Run(args);
    }
}