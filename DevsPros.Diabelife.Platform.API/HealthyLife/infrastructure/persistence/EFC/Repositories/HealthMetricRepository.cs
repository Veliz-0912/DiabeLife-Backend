using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class HealthMetricRepository : IHealthMetricRepository
{
    private readonly AppDbContext _context;

    public HealthMetricRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HealthMetric>> GetAllAsync()
    {
        return await _context.HealthMetrics.OrderByDescending(h => h.CreatedAt).ToListAsync();
    }

    public async Task<HealthMetric?> GetByIdAsync(int id)
    {
        return await _context.HealthMetrics.FindAsync(id);
    }

    public async Task<HealthMetric> AddAsync(HealthMetric healthMetric)
    {
        await _context.HealthMetrics.AddAsync(healthMetric);
        await _context.SaveChangesAsync();
        return healthMetric;
    }

    public async Task<HealthMetric> UpdateAsync(HealthMetric healthMetric)
    {
        _context.HealthMetrics.Update(healthMetric);
        await _context.SaveChangesAsync();
        return healthMetric;
    }

    public async Task DeleteAsync(int id)
    {
        var healthMetric = await _context.HealthMetrics.FindAsync(id);
        if (healthMetric != null)
        {
            _context.HealthMetrics.Remove(healthMetric);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<HealthMetric?> GetLatestAsync()
    {
        return await _context.HealthMetrics
            .OrderByDescending(h => h.CreatedAt)
            .FirstOrDefaultAsync();
    }
}