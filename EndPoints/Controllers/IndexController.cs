using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndexController : ControllerBase
{
    [HttpGet]
    IActionResult Index()
    {
        return Ok(new { Success = true, Data = "Тест" });
    }
}