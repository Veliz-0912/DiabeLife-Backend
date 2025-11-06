using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;

public interface IHealthMetricCommandService
{
    Task<HealthMetric> Handle(CreateHealthMetricCommand command);
    Task<HealthMetric> Handle(UpdateHealthMetricCommand command);
    Task<bool> Handle(int id);
}