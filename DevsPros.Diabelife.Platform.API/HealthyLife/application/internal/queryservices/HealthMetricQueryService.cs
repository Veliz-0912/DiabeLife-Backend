using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.QueryServices;

internal class HealthMetricQueryService : IHealthMetricQueryService
{
    private readonly IHealthMetricRepository _healthMetricRepository;

    public HealthMetricQueryService(IHealthMetricRepository healthMetricRepository)
    {
        _healthMetricRepository = healthMetricRepository;
    }

    public async Task<IEnumerable<HealthMetric>> Handle()
    {
        return await _healthMetricRepository.GetAllAsync();
    }

    public async Task<HealthMetric?> Handle(int id)
    {
        return await _healthMetricRepository.GetByIdAsync(id);
    }

    public async Task<HealthMetric?> GetLatestHealthMetric()
    {
        return await _healthMetricRepository.GetLatestAsync();
    }
}