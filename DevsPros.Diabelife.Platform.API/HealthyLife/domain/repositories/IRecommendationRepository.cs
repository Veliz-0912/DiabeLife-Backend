using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

public interface IRecommendationRepository
{
    Task<IEnumerable<Recommendation>> GetAllAsync();
    Task<Recommendation?> GetByIdAsync(int id);
    Task<Recommendation> AddAsync(Recommendation recommendation);
    Task<Recommendation> UpdateAsync(Recommendation recommendation);
    Task DeleteAsync(int id);
}