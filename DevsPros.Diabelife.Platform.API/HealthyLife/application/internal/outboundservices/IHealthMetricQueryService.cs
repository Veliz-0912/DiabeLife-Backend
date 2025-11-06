using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;

public interface IHealthMetricQueryService
{
    Task<IEnumerable<HealthMetric>> Handle();
    Task<HealthMetric?> Handle(int id);
    Task<HealthMetric?> GetLatestHealthMetric();
}