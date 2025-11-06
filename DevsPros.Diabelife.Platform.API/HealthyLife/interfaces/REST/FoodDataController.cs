using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Available Food Data Endpoints")]
public class FoodDataController : ControllerBase
{
    private readonly IFoodDataCommandService _foodDataCommandService;
    private readonly IFoodDataQueryService _foodDataQueryService;

    public FoodDataController(
        IFoodDataCommandService foodDataCommandService,
        IFoodDataQueryService foodDataQueryService)
    {
        _foodDataCommandService = foodDataCommandService;
        _foodDataQueryService = foodDataQueryService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all food data",
        Description = "Get all food data entries from the database",
        OperationId = "GetAllFoodData")]
    [SwaggerResponse(200, "Food data returned successfully", typeof(IEnumerable<FoodData>))]
    public async Task<ActionResult<IEnumerable<FoodData>>> GetAllFoodData()
    {
        var foodData = await _foodDataQueryService.Handle();
        return Ok(foodData);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get food data by id",
        Description = "Get a specific food data entry by its id",
        OperationId = "GetFoodDataById")]
    [SwaggerResponse(200, "Food data returned successfully", typeof(FoodData))]
    [SwaggerResponse(404, "Food data not found")]
    public async Task<ActionResult<FoodData>> GetFoodDataById(int id)
    {
        var foodData = await _foodDataQueryService.Handle(id);
        if (foodData == null)
            return NotFound();
        return Ok(foodData);
    }

    [HttpGet("recent")]
    [SwaggerOperation(
        Summary = "Get recent food data",
        Description = "Get the most recent food data entries",
        OperationId = "GetRecentFoodData")]
    [SwaggerResponse(200, "Recent food data returned successfully", typeof(IEnumerable<FoodData>))]
    public async Task<ActionResult<IEnumerable<FoodData>>> GetRecentFoodData([FromQuery] int count = 10)
    {
        var foodData = await _foodDataQueryService.GetRecentFoods(count);
        return Ok(foodData);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create food data",
        Description = "Create a new food data entry",
        OperationId = "CreateFoodData")]
    [SwaggerResponse(201, "Food data created successfully", typeof(FoodData))]
    [SwaggerResponse(400, "Invalid food data")]
    public async Task<ActionResult<FoodData>> CreateFoodData([FromBody] CreateFoodDataCommand command)
    {
        var foodData = await _foodDataCommandService.Handle(command);
        return CreatedAtAction(nameof(GetFoodDataById), new { id = foodData.Id }, foodData);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update food data",
        Description = "Update an existing food data entry",
        OperationId = "UpdateFoodData")]
    [SwaggerResponse(200, "Food data updated successfully", typeof(FoodData))]
    [SwaggerResponse(404, "Food data not found")]
    public async Task<ActionResult<FoodData>> UpdateFoodData(int id, [FromBody] UpdateFoodDataCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var foodData = await _foodDataCommandService.Handle(command);
            return Ok(foodData);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete food data",
        Description = "Delete a food data entry",
        OperationId = "DeleteFoodData")]
    [SwaggerResponse(204, "Food data deleted successfully")]
    [SwaggerResponse(404, "Food data not found")]
    public async Task<ActionResult> DeleteFoodData(int id)
    {
        var result = await _foodDataCommandService.Handle(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}