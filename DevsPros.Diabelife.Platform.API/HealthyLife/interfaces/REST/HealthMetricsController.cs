using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Available Health Metrics Endpoints")]
public class HealthMetricsController : ControllerBase
{
    private readonly IHealthMetricCommandService _healthMetricCommandService;
    private readonly IHealthMetricQueryService _healthMetricQueryService;

    public HealthMetricsController(
        IHealthMetricCommandService healthMetricCommandService,
        IHealthMetricQueryService healthMetricQueryService)
    {
        _healthMetricCommandService = healthMetricCommandService;
        _healthMetricQueryService = healthMetricQueryService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all health metrics",
        Description = "Get all health metrics from the database",
        OperationId = "GetAllHealthMetrics")]
    [SwaggerResponse(200, "Health metrics returned successfully", typeof(IEnumerable<HealthMetric>))]
    public async Task<ActionResult<IEnumerable<HealthMetric>>> GetAllHealthMetrics()
    {
        var healthMetrics = await _healthMetricQueryService.Handle();
        return Ok(healthMetrics);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get health metric by id",
        Description = "Get a specific health metric by its id",
        OperationId = "GetHealthMetricById")]
    [SwaggerResponse(200, "Health metric returned successfully", typeof(HealthMetric))]
    [SwaggerResponse(404, "Health metric not found")]
    public async Task<ActionResult<HealthMetric>> GetHealthMetricById(int id)
    {
        var healthMetric = await _healthMetricQueryService.Handle(id);
        if (healthMetric == null)
            return NotFound();
        return Ok(healthMetric);
    }

    [HttpGet("latest")]
    [SwaggerOperation(
        Summary = "Get latest health metric",
        Description = "Get the most recent health metric entry",
        OperationId = "GetLatestHealthMetric")]
    [SwaggerResponse(200, "Latest health metric returned successfully", typeof(HealthMetric))]
    [SwaggerResponse(404, "No health metrics found")]
    public async Task<ActionResult<HealthMetric>> GetLatestHealthMetric()
    {
        var healthMetric = await _healthMetricQueryService.GetLatestHealthMetric();
        if (healthMetric == null)
            return NotFound();
        return Ok(healthMetric);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create health metric",
        Description = "Create a new health metric entry",
        OperationId = "CreateHealthMetric")]
    [SwaggerResponse(201, "Health metric created successfully", typeof(HealthMetric))]
    [SwaggerResponse(400, "Invalid health metric data")]
    public async Task<ActionResult<HealthMetric>> CreateHealthMetric([FromBody] CreateHealthMetricCommand command)
    {
        var healthMetric = await _healthMetricCommandService.Handle(command);
        return CreatedAtAction(nameof(GetHealthMetricById), new { id = healthMetric.Id }, healthMetric);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update health metric",
        Description = "Update an existing health metric entry",
        OperationId = "UpdateHealthMetric")]
    [SwaggerResponse(200, "Health metric updated successfully", typeof(HealthMetric))]
    [SwaggerResponse(404, "Health metric not found")]
    public async Task<ActionResult<HealthMetric>> UpdateHealthMetric(int id, [FromBody] UpdateHealthMetricCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var healthMetric = await _healthMetricCommandService.Handle(command);
            return Ok(healthMetric);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete health metric",
        Description = "Delete a health metric entry",
        OperationId = "DeleteHealthMetric")]
    [SwaggerResponse(204, "Health metric deleted successfully")]
    [SwaggerResponse(404, "Health metric not found")]
    public async Task<ActionResult> DeleteHealthMetric(int id)
    {
        var result = await _healthMetricCommandService.Handle(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}