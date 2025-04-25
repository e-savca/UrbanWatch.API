using Microsoft.AspNetCore.Mvc;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class VehiclesController : ControllerBase
{
    public VehiclesController(

        )
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
}