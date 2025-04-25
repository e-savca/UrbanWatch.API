using Microsoft.AspNetCore.Mvc;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;
[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class RoutesController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}