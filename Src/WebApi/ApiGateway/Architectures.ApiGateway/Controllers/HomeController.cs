using Microsoft.AspNetCore.Mvc;

namespace Architectures.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Success ...");
    }
}
