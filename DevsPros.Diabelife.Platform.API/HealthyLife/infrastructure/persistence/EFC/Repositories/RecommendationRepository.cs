using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

public class RecommendationRepository : IRecommendationRepository
{
    private readonly AppDbContext _context;

    public RecommendationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recommendation>> GetAllAsync()
    {
        return await _context.Recommendations.OrderByDescending(r => r.CreatedAt).ToListAsync();
    }

    public async Task<Recommendation?> GetByIdAsync(int id)
    {
        return await _context.Recommendations.FindAsync(id);
    }

    public async Task<Recommendation> AddAsync(Recommendation recommendation)
    {
        await _context.Recommendations.AddAsync(recommendation);
        await _context.SaveChangesAsync();
        return recommendation;
    }

    public async Task<Recommendation> UpdateAsync(Recommendation recommendation)
    {
        _context.Recommendations.Update(recommendation);
        await _context.SaveChangesAsync();
        return recommendation;
    }

    public async Task DeleteAsync(int id)
    {
        var recommendation = await _context.Recommendations.FindAsync(id);
        if (recommendation != null)
        {
            _context.Recommendations.Remove(recommendation);
            await _context.SaveChangesAsync();
        }
    }
}