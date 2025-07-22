using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api_Solution.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAllOrigins")] // Apply CORS policy at base level
public abstract class BaseController : ControllerBase
{

    protected IActionResult ApiOk(object? data, string message = "Success")
    {
        return Ok(new ApiResponse<object>(true, message, data));
    }

    protected IActionResult ApiError(string message, object? errors = null)
    {
        return BadRequest(new ApiResponse<object>(false, message, errors));
    }

    protected IActionResult ApiCreated(object? data, string message = "Created", string uri = "")
    {
        return Created(uri, new ApiResponse<object>(true, message, data));
    }

    protected IActionResult ApiNotFound(string message = "Not Found")
    {
        return NotFound(new ApiResponse<object>(false, message, null));
    }

    // Override default Ok if you want consistent response format
    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        //return base.Ok(new ApiResponse<object>(true, "Success", value));
        return base.Ok(value);
    }

}

// Generic API response model
public class ApiResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    public ApiResponse(bool status, string message, T? data)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}
