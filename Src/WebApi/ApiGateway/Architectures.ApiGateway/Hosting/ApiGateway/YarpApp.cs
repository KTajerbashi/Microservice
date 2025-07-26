using Ocelot.Values;
using Yarp.ReverseProxy.Transforms;

namespace Architectures.ApiGateway.Hosting.ApiGateway;

public static class YarpApp
{
    public static WebApplication StartYarp(this WebApplicationBuilder builder)
    {
        // Add YARP services
        //builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        builder.Services
           .AddReverseProxy()
           .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
           .AddTransforms(builderContext =>
           {
               builderContext.AddRequestTransform(async context =>
               {
                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                   logger.LogInformation($"Request from {context.HttpContext.Connection.RemoteIpAddress} to {context.HttpContext.Request.Path}");
               });
               builderContext.AddResponseTransform(async context =>
               {
                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                   logger.LogInformation($"Response for {context.HttpContext.Request.Path} - Status: {context.HttpContext.Response.StatusCode}");
               });
           });


        return builder.Build();
    }
    public static void RunYarpServer(this WebApplication app)
    {
        // Map reverse proxy routes
        app.MapReverseProxy();

        app.Run();
    }
}
