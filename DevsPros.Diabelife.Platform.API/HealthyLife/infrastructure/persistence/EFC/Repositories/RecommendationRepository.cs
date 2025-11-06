using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class RecommendationRepository : BaseRepository<Recommendation>, IRecommendationRepository
{
    public RecommendationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Recommendation>> GetAllAsync()
    {
        return await Context.Recommendations.OrderByDescending(r => r.CreatedAt).ToListAsync();
    }

    public async Task<Recommendation?> GetByIdAsync(int id)
    {
        return await Context.Recommendations.FindAsync(id);
    }

    public async Task<Recommendation> AddAsync(Recommendation recommendation)
    {
        await Context.Recommendations.AddAsync(recommendation);
        await Context.SaveChangesAsync();
        return recommendation;
    }

    public async Task<Recommendation> UpdateAsync(Recommendation recommendation)
    {
        Context.Recommendations.Update(recommendation);
        await Context.SaveChangesAsync();
        return recommendation;
    }

    public async Task DeleteAsync(int id)
    {
        var recommendation = await Context.Recommendations.FindAsync(id);
        if (recommendation != null)
        {
            Context.Recommendations.Remove(recommendation);
            await Context.SaveChangesAsync();
        }
    }
}