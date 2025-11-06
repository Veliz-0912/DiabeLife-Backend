using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class FoodDataRepository : IFoodDataRepository
{
    private readonly AppDbContext _context;

    public FoodDataRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FoodData>> GetAllAsync()
    {
        return await _context.FoodData.OrderByDescending(f => f.Timestamp).ToListAsync();
    }

    public async Task<FoodData?> GetByIdAsync(int id)
    {
        return await _context.FoodData.FindAsync(id);
    }

    public async Task<FoodData> AddAsync(FoodData foodData)
    {
        await _context.FoodData.AddAsync(foodData);
        await _context.SaveChangesAsync();
        return foodData;
    }

    public async Task<FoodData> UpdateAsync(FoodData foodData)
    {
        _context.FoodData.Update(foodData);
        await _context.SaveChangesAsync();
        return foodData;
    }

    public async Task DeleteAsync(int id)
    {
        var foodData = await _context.FoodData.FindAsync(id);
        if (foodData != null)
        {
            _context.FoodData.Remove(foodData);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<FoodData>> GetRecentFoodsAsync(int count = 10)
    {
        return await _context.FoodData
            .OrderByDescending(f => f.Timestamp)
            .Take(count)
            .ToListAsync();
    }
}