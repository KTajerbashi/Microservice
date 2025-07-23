using System.Threading.RateLimiting;
using Yarp.ReverseProxy.Transforms;

namespace Architectures.ApiGateway.Hosting.Configuration;

public static class HostConfiguration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        //  Rate Limiting
        builder.Services.AddRateLimiter(options =>
        {
            options.AddPolicy<string>("apiPolicy", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });

        //  Caching
        builder.Services.AddOutputCache(options =>
        {
            options.AddPolicy("Expire300", builder => builder.Expire(TimeSpan.FromSeconds(300)));
        });

        builder.Services.AddControllers();


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
        return builder;
    }
    public static WebApplication AddPipelineConfiguration(this WebApplication app)
    {

        app.MapControllers();
        return app;
    }
}
