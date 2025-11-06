using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;

public interface IFoodDataCommandService
{
    Task<FoodData> Handle(CreateFoodDataCommand command);
    Task<FoodData> Handle(UpdateFoodDataCommand command);
    Task<bool> Handle(int id);
}

public interface IFoodDataQueryService
{
    Task<IEnumerable<FoodData>> Handle();
    Task<FoodData?> Handle(int id);
    Task<IEnumerable<FoodData>> GetRecentFoods(int count = 10);
}