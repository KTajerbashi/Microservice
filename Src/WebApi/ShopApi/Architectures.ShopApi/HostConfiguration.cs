using Architectures.Utilities;

namespace Architectures.ShopApi;

public static class HostConfiguration
{
    public static WebApplication AddMinimalApis(this WebApplication app)
    {
        var baseUrl = app.Configuration["BaseUrl"] ?? "#";

        var dateTime = DateTime.Now;

        app.MapGet("/", () =>
        {

            var html = HTMLPageExtensions.GenerateStatusHtml("🛒 Shop Server", "#ff6565", "#ffe000", baseUrl, dateTime);

            return Results.Content(html, "text/html");
        });
        return app;
    }

}
