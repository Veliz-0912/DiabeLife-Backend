using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Available Recommendations Endpoints")]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationCommandService _recommendationCommandService;
    private readonly IRecommendationQueryService _recommendationQueryService;

    public RecommendationsController(
        IRecommendationCommandService recommendationCommandService,
        IRecommendationQueryService recommendationQueryService)
    {
        _recommendationCommandService = recommendationCommandService;
        _recommendationQueryService = recommendationQueryService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all recommendations",
        Description = "Get all recommendations from the database",
        OperationId = "GetAllRecommendations")]
    [SwaggerResponse(200, "Recommendations returned successfully", typeof(IEnumerable<Recommendation>))]
    public async Task<ActionResult<IEnumerable<Recommendation>>> GetAllRecommendations()
    {
        var recommendations = await _recommendationQueryService.Handle();
        return Ok(recommendations);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get recommendation by id",
        Description = "Get a specific recommendation by its id",
        OperationId = "GetRecommendationById")]
    [SwaggerResponse(200, "Recommendation returned successfully", typeof(Recommendation))]
    [SwaggerResponse(404, "Recommendation not found")]
    public async Task<ActionResult<Recommendation>> GetRecommendationById(int id)
    {
        var recommendation = await _recommendationQueryService.Handle(id);
        if (recommendation == null)
            return NotFound();
        return Ok(recommendation);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create recommendation",
        Description = "Create a new recommendation entry",
        OperationId = "CreateRecommendation")]
    [SwaggerResponse(201, "Recommendation created successfully", typeof(Recommendation))]
    [SwaggerResponse(400, "Invalid recommendation data")]
    public async Task<ActionResult<Recommendation>> CreateRecommendation([FromBody] CreateRecommendationCommand command)
    {
        var recommendation = await _recommendationCommandService.Handle(command);
        return CreatedAtAction(nameof(GetRecommendationById), new { id = recommendation.Id }, recommendation);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update recommendation",
        Description = "Update an existing recommendation entry",
        OperationId = "UpdateRecommendation")]
    [SwaggerResponse(200, "Recommendation updated successfully", typeof(Recommendation))]
    [SwaggerResponse(404, "Recommendation not found")]
    public async Task<ActionResult<Recommendation>> UpdateRecommendation(int id, [FromBody] UpdateRecommendationCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var recommendation = await _recommendationCommandService.Handle(command);
            return Ok(recommendation);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete recommendation",
        Description = "Delete a recommendation entry",
        OperationId = "DeleteRecommendation")]
    [SwaggerResponse(204, "Recommendation deleted successfully")]
    [SwaggerResponse(404, "Recommendation not found")]
    public async Task<ActionResult> DeleteRecommendation(int id)
    {
        var result = await _recommendationCommandService.Handle(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}