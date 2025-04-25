using Microsoft.AspNetCore.Mvc;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class StopTimesController : ControllerBase
{
    public StopTimesController(

    )
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
}