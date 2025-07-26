using Architectures.Utilities;

namespace Architectures.SupportApi;

public static class HostConfiguration
{
    public static WebApplication AddMinimalApis(this WebApplication app)
    {
        var baseUrl = app.Configuration["BaseUrl"] ?? "#";
        
        var dateTime = DateTime.Now;

        app.MapGet("/", () =>
        {

            var html = HTMLPageExtensions.GenerateStatusHtml("🛠️ Support Server", "#65ffbb", "#0090ff", baseUrl, dateTime);

            return Results.Content(html, "text/html");
        });

        return app;
    }

}
