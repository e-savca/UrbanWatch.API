using Microsoft.AspNetCore.Mvc;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class TripsController : ControllerBase
{
    public TripsController(

    )
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
}