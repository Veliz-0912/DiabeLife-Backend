using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

public interface IHealthMetricRepository
{
    Task<IEnumerable<HealthMetric>> GetAllAsync();
    Task<HealthMetric?> GetByIdAsync(int id);
    Task<HealthMetric> AddAsync(HealthMetric healthMetric);
    Task<HealthMetric> UpdateAsync(HealthMetric healthMetric);
    Task DeleteAsync(int id);
    Task<HealthMetric?> GetLatestAsync();
}