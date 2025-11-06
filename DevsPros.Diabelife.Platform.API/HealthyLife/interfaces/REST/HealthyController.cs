using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Healthy Life Dashboard Endpoints")]
public class HealthyController : ControllerBase
{
    private readonly IHealthMetricQueryService _healthMetricQueryService;
    private readonly IRecommendationQueryService _recommendationQueryService;
    private readonly IFoodDataQueryService _foodDataQueryService;

    public HealthyController(
        IHealthMetricQueryService healthMetricQueryService,
        IRecommendationQueryService recommendationQueryService,
        IFoodDataQueryService foodDataQueryService)
    {
        _healthMetricQueryService = healthMetricQueryService;
        _recommendationQueryService = recommendationQueryService;
        _foodDataQueryService = foodDataQueryService;
    }

    [HttpGet("dashboard")]
    [SwaggerOperation(
        Summary = "Get complete dashboard data",
        Description = "Get all data needed for the healthy life dashboard including latest metrics, recommendations, and recent foods",
        OperationId = "GetHealthyDashboard")]
    [SwaggerResponse(200, "Dashboard data returned successfully")]
    public async Task<ActionResult> GetHealthyDashboard()
    {
        var latestHealthMetric = await _healthMetricQueryService.GetLatestHealthMetric();
        var recommendations = await _recommendationQueryService.Handle();
        var recentFoods = await _foodDataQueryService.GetRecentFoods(10);

        var response = new
        {
            healthy = latestHealthMetric != null ? new[] { latestHealthMetric } : new HealthMetric[0],
            recommendations = recommendations,
            foodData = recentFoods
        };

        return Ok(response);
    }

    [HttpGet("metrics/latest")]
    [SwaggerOperation(
        Summary = "Get latest health metrics",
        Description = "Get the most recent health metrics for the dashboard summary",
        OperationId = "GetLatestHealthMetrics")]
    [SwaggerResponse(200, "Latest health metrics returned successfully")]
    public async Task<ActionResult> GetLatestHealthMetrics()
    {
        var latestHealthMetric = await _healthMetricQueryService.GetLatestHealthMetric();
        
        if (latestHealthMetric == null)
        {
            return Ok(new
            {
                heartRate = 0,
                glucose = 0,
                weight = 0,
                bloodPressure = "0/0"
            });
        }

        return Ok(new
        {
            heartRate = latestHealthMetric.HeartRate,
            glucose = latestHealthMetric.Glucose,
            weight = latestHealthMetric.Weight,
            bloodPressure = latestHealthMetric.BloodPressure
        });
    }

    [HttpGet("summary")]
    [SwaggerOperation(
        Summary = "Get health summary",
        Description = "Get health summary with formatted metrics for the dashboard",
        OperationId = "GetHealthSummary")]
    [SwaggerResponse(200, "Health summary returned successfully")]
    public async Task<ActionResult> GetHealthSummary()
    {
        var latestHealthMetric = await _healthMetricQueryService.GetLatestHealthMetric();
        
        if (latestHealthMetric == null)
        {
            return Ok(new
            {
                heartRate = new { value = 0, unit = "bpm" },
                glucose = new { value = 0, unit = "mg/dL" },
                weight = new { value = 0, unit = "kg" },
                bloodPressure = new { value = "0/0", unit = "mmHg" }
            });
        }

        return Ok(new
        {
            heartRate = new { value = latestHealthMetric.HeartRate, unit = "bpm" },
            glucose = new { value = latestHealthMetric.Glucose, unit = "mg/dL" },
            weight = new { value = latestHealthMetric.Weight, unit = "kg" },
            bloodPressure = new { value = latestHealthMetric.BloodPressure, unit = "mmHg" }
        });
    }
}