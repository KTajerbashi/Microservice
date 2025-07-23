namespace Architectures.ShopApi.Services;

public class ClientDataService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientDataService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetBlazorClientData()
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var response = await client.GetAsync("/blazor/api/data");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetMvcClientData()
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var response = await client.GetAsync("/mvc/api/data");
        return await response.Content.ReadAsStringAsync();
    }
}