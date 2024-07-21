using FamilySync.Core.Abstractions;
using FamilySync.Core.Abstractions.Options;
using FamilySync.Core.Example.Persistence;
using FamilySync.Core.Example.Services;
using FamilySync.Core.Persistence.Extensions;

namespace FamilySync.Core.Example;

public class Config : ServiceConfiguration
{
    public override void Configure(IApplicationBuilder builder)
    {
        
    }
    
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AuthenticationOptions>(Configuration.GetSection("Config:Authentication"));
        
        services.AddMySQLContext<ExampleContext>("example", Configuration);

        services.AddScoped<IExampleService, ExampleService>();
    }
}