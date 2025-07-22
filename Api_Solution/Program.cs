using Api_Solution.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<UserController>();

builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CanncellationToken API", Version = "v1" });

    // Add support for XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //options.IncludeXmlComments(xmlPath);
});

// Add CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());

    options.AddPolicy("AllowMvcClient",
        builder => builder.WithOrigins("https://localhost:7244")
                         .AllowAnyMethod()
                         .AllowAnyHeader());

    options.AddPolicy("AllowBlazorClient",
        builder => builder.WithOrigins("https://localhost:7182")
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
        options.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
    });
}

app.UseCors("AllowAllOrigins"); // Or whichever policy you prefer

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
