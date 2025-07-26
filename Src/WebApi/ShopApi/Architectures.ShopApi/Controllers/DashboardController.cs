using Architectures.ShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.ShopApi.Controllers;
public class DashboardController : BaseContorller
{
    private readonly ClientDataService _clientDataService;

    public DashboardController(ClientDataService clientDataService)
    {
        _clientDataService = clientDataService;
    }

    [HttpGet("client-data")]
    public async Task<IActionResult> GetClientData()
    {
        var blazorData = await _clientDataService.GetBlazorClientData();
        var mvcData = await _clientDataService.GetMvcClientData();

        return Ok(new
        {
            BlazorData = blazorData,
            MvcData = mvcData
        });
    }
}