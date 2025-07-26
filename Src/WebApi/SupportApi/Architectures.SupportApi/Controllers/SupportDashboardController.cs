using Architectures.SupportApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.SupportApi.Controllers;

[ApiController]
[Route("support/[controller]")]
public class BaseContorller : ControllerBase
{

}

[ApiController]
[Route("contact")]
public class ContactController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(new { Id = id, Message = $"Contact {id}" });
    }
}



public class SupportDashboardController : BaseContorller
{
    private readonly ClientDataService _clientDataService;

    public SupportDashboardController(ClientDataService clientDataService)
    {
        _clientDataService = clientDataService;
    }

    [HttpGet("client-status")]
    public async Task<IActionResult> GetClientStatus()
    {
        var blazorData = await _clientDataService.GetBlazorClientData();
        var mvcData = await _clientDataService.GetMvcClientData();

        return Ok(new
        {
            BlazorStatus = !string.IsNullOrEmpty(blazorData),
            MvcStatus = !string.IsNullOrEmpty(mvcData),
            LastUpdated = DateTime.UtcNow
        });
    }
}