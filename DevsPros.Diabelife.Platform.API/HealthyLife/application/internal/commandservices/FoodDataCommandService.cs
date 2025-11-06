using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

internal class FoodDataCommandService : IFoodDataCommandService
{
    private readonly IFoodDataRepository _foodDataRepository;

    public FoodDataCommandService(IFoodDataRepository foodDataRepository)
    {
        _foodDataRepository = foodDataRepository;
    }

    public async Task<FoodData> Handle(CreateFoodDataCommand command)
    {
        var foodData = command.Timestamp.HasValue 
            ? new FoodData(command.Food, command.Timestamp.Value) 
            : new FoodData(command.Food);
        return await _foodDataRepository.AddAsync(foodData);
    }

    public async Task<FoodData> Handle(UpdateFoodDataCommand command)
    {
        var existingFoodData = await _foodDataRepository.GetByIdAsync(command.Id);
        if (existingFoodData == null)
            throw new Exception($"FoodData with id {command.Id} not found");

        existingFoodData.UpdateFood(command.Food);
        return await _foodDataRepository.UpdateAsync(existingFoodData);
    }

    public async Task<bool> Handle(int id)
    {
        try
        {
            await _foodDataRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}