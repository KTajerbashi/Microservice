using Microsoft.AspNetCore.Mvc;

namespace Architectures.ShopApi.Controllers;

[ApiController]
[Route("shop/[controller]")]
public abstract class BaseContorller : ControllerBase
{

}
