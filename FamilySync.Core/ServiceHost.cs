﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

namespace FamilySync.Core;

public static class ServiceHost<TEntryPoint> where TEntryPoint : EntryPoint, new()
{
    public static int Run(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var loggerConfig = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext();
        
        var telemetryEnabled = bool.Parse(config["Config:TelemetryLogging:Enabled"]!);
        if (telemetryEnabled)
        {
            Log.Logger = loggerConfig
                .WriteTo.Console()
                .WriteTo.OpenTelemetry(x =>
                {
                    x.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
                    x.Protocol = OtlpProtocol.HttpProtobuf;
                    x.Headers = new Dictionary<string, string> 
                    {
                        ["X-Seq-ApiKey"] = "5zlOgtgspIxPldE3uxdg"
                    };
                    x.ResourceAttributes = new Dictionary<string, object>
                    {
                        ["service.name"] = config["Config:Service:Name"]!
                    };

                })
                .CreateLogger();
        }
        else
        {
            Log.Logger = loggerConfig
                .WriteTo.Console()
                .CreateLogger();
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
                    // TODO: Implement handling of cloud based database migration
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
            Log.Fatal(ex, "Fatal error at application startup!");
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