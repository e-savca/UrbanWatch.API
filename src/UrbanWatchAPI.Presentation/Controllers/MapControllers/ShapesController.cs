using Microsoft.AspNetCore.Mvc;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class ShapesController : ControllerBase
{
    public ShapesController(

    )
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
}