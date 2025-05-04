using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Application.PublicTransport.Routes.Queries.GetAllRoutes;
using Route = UrbanWatchAPI.Domain.Entities.PublicTransportEntities.Route;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class RoutesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRoutes()
    {
        var query = new GetAllRoutesQuery();
        var routes = await mediator.Send(query);
        
        // TODO: return list of routes from MongoDB
        return Ok(new
        {
            message = "List of all routes", count = routes.Count.ToString(), routes
        });
    }

    [HttpGet("{routeId:int}")]
    public IActionResult GetRouteById(int routeId)
    {
        // TODO: return specific route by ID from MongoDB
        return Ok(new { message = $"Details for route {routeId}" });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoutes([FromBody] List<Route> routes)
    {
        if (routes == null || routes.Count == 0)
            return BadRequest("Routes list cannot be empty.");

        // TODO: actualizează toate rutele în MongoDB
        // ex: await _routesService.ReplaceAllRoutesAsync(routes);

        return NoContent(); // 204 - succes, fără răspuns în body
    }
}