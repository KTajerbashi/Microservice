using Architectures.ApiGateway.Hosting.ApiGateway;
using Architectures.ApiGateway.Hosting.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder
    .AddServices()
    .StartYarp()
    .AddPipelineConfiguration()
    .RunYarpServer()
    ;
//builder.StartOcelot().RunOcelopServer();

