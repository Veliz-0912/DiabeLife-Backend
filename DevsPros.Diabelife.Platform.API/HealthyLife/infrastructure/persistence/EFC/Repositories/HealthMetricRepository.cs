using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class HealthMetricRepository : BaseRepository<HealthMetric>, IHealthMetricRepository
{
    public HealthMetricRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<HealthMetric>> GetAllAsync()
    {
        return await Context.HealthMetrics.OrderByDescending(h => h.CreatedAt).ToListAsync();
    }

    public async Task<HealthMetric?> GetByIdAsync(int id)
    {
        return await Context.HealthMetrics.FindAsync(id);
    }

    public async Task<HealthMetric> AddAsync(HealthMetric healthMetric)
    {
        await Context.HealthMetrics.AddAsync(healthMetric);
        await Context.SaveChangesAsync();
        return healthMetric;
    }

    public async Task<HealthMetric> UpdateAsync(HealthMetric healthMetric)
    {
        Context.HealthMetrics.Update(healthMetric);
        await Context.SaveChangesAsync();
        return healthMetric;
    }

    public async Task DeleteAsync(int id)
    {
        var healthMetric = await Context.HealthMetrics.FindAsync(id);
        if (healthMetric != null)
        {
            Context.HealthMetrics.Remove(healthMetric);
            await Context.SaveChangesAsync();
        }
    }

    public async Task<HealthMetric?> GetLatestAsync()
    {
        return await Context.HealthMetrics
            .OrderByDescending(h => h.CreatedAt)
            .FirstOrDefaultAsync();
    }
}