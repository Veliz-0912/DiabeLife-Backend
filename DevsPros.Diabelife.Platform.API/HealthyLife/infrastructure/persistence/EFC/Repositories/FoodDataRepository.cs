using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class FoodDataRepository : BaseRepository<FoodData>, IFoodDataRepository
{
    public FoodDataRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<FoodData>> GetAllAsync()
    {
        return await Context.FoodData.OrderByDescending(f => f.Timestamp).ToListAsync();
    }

    public async Task<FoodData?> GetByIdAsync(int id)
    {
        return await Context.FoodData.FindAsync(id);
    }

    public async Task<FoodData> AddAsync(FoodData foodData)
    {
        await Context.FoodData.AddAsync(foodData);
        await Context.SaveChangesAsync();
        return foodData;
    }

    public async Task<FoodData> UpdateAsync(FoodData foodData)
    {
        Context.FoodData.Update(foodData);
        await Context.SaveChangesAsync();
        return foodData;
    }

    public async Task DeleteAsync(int id)
    {
        var foodData = await Context.FoodData.FindAsync(id);
        if (foodData != null)
        {
            Context.FoodData.Remove(foodData);
            await Context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<FoodData>> GetRecentFoodsAsync(int count = 10)
    {
        return await Context.FoodData
            .OrderByDescending(f => f.Timestamp)
            .Take(count)
            .ToListAsync();
    }
}