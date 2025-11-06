using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.QueryServices;

internal class FoodDataQueryService : IFoodDataQueryService
{
    private readonly IFoodDataRepository _foodDataRepository;

    public FoodDataQueryService(IFoodDataRepository foodDataRepository)
    {
        _foodDataRepository = foodDataRepository;
    }

    public async Task<IEnumerable<FoodData>> Handle()
    {
        return await _foodDataRepository.GetAllAsync();
    }

    public async Task<FoodData?> Handle(int id)
    {
        return await _foodDataRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<FoodData>> GetRecentFoods(int count = 10)
    {
        return await _foodDataRepository.GetRecentFoodsAsync(count);
    }
}