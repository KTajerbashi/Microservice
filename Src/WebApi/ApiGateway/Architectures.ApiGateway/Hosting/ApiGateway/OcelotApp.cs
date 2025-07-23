using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Architectures.ApiGateway.Hosting.ApiGateway;

public static class OcelotApp
{
    public static WebApplication StartOcelot(this WebApplicationBuilder builder)
    {

        // Add Ocelot services
        builder.Services.AddOcelot();

        // Add configuration file
        builder.Configuration.AddJsonFile("ocelot.json");

        return builder.Build();
    }
    public static void RunOcelopServer(this WebApplication app)
    {
        app.UseOcelot().Wait();

        app.Run();
    }
}
