using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("[action]")]
    public IActionResult Index()
    {
        return Content("Verbose API");
    }
}