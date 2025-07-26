using Microsoft.AspNetCore.Mvc;

namespace Architectures.ShopApi.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(new { Id = id, Name = $"Product {id}" });
    }
}
