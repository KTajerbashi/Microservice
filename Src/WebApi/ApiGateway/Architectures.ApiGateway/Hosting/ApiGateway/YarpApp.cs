namespace Architectures.ApiGateway.Hosting.ApiGateway;

public static class YarpApp
{
    public static WebApplication StartYarp(this WebApplicationBuilder builder)
    {
        // Add YARP services
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        return builder.Build();
    }
    public static void RunYarpServer(this WebApplication app)
    {
        // Map reverse proxy routes
        app.MapReverseProxy();

        app.Run();
    }
}
