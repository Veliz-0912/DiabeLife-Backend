using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

public interface IFoodDataRepository
{
    Task<IEnumerable<FoodData>> GetAllAsync();
    Task<FoodData?> GetByIdAsync(int id);
    Task<FoodData> AddAsync(FoodData foodData);
    Task<FoodData> UpdateAsync(FoodData foodData);
    Task DeleteAsync(int id);
    Task<IEnumerable<FoodData>> GetRecentFoodsAsync(int count = 10);
}