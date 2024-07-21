using FamilySync.Core.Abstractions.Filters;
using FamilySync.Core.Abstractions.Options;
using FamilySync.Core.Authentication.Extensions;
using FamilySync.Core.Options;
using FastExpressionCompiler;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FamilySync.Core.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceOptions>(configuration.GetRequiredSection(ServiceOptions.Section));
        services.Configure<InclusionOptions>(configuration.GetSection(InclusionOptions.Section));
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.Section));

        var config = configuration.GetRequiredSection(ConfigOptions.Section).Get<ConfigOptions>()!;

        if (config.Inclusion.Versioning)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";
            });
        }

        if (config.Inclusion.Authorization)
        {
            services.AddAuth(config.Authentication);
        }
        
        if (config.Inclusion.MVC)
        {
            var mvcBuilder = services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.Filters.Add<ExceptionFilter>();
            });
            
            // Since this is a used as a NuGet we need to scan all assemblies and add them
            // This makes it possible for MVC to discover and use controllers and other MVC components and not just the main app assembly
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }
        }

        if (config.Inclusion.Swagger)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

            services.AddSwaggerGen(options =>
            {
                options.UseAllOfToExtendReferenceSchemas();
                options.CustomOperationIds(x => $"{x.ActionDescriptor.RouteValues["action"]}");

                var definitionID = "httpBearer"; 
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization Header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new()
                    {
                        Id = definitionID,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(definitionID, securityScheme);
                options.AddSecurityRequirement(new()
                {
                    { securityScheme, new List<string>() }
                });
            });

            services.AddEndpointsApiExplorer();
        }
        
        if (config.Inclusion.Mapper)
        {
            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = false;

            services.AddSingleton<IMapper, ServiceMapper>();
        }

        return services;
    }
}