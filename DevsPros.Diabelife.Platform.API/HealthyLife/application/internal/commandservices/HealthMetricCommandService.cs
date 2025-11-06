using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

internal class HealthMetricCommandService : IHealthMetricCommandService
{
    private readonly IHealthMetricRepository _healthMetricRepository;

    public HealthMetricCommandService(IHealthMetricRepository healthMetricRepository)
    {
        _healthMetricRepository = healthMetricRepository;
    }

    public async Task<HealthMetric> Handle(CreateHealthMetricCommand command)
    {
        var healthMetric = new HealthMetric(command.HeartRate, command.Glucose, command.Weight, command.BloodPressure);
        return await _healthMetricRepository.AddAsync(healthMetric);
    }

    public async Task<HealthMetric> Handle(UpdateHealthMetricCommand command)
    {
        var existingHealthMetric = await _healthMetricRepository.GetByIdAsync(command.Id);
        if (existingHealthMetric == null)
            throw new Exception($"HealthMetric with id {command.Id} not found");

        existingHealthMetric.UpdateHealthData(command.HeartRate, command.Glucose, command.Weight, command.BloodPressure);
        return await _healthMetricRepository.UpdateAsync(existingHealthMetric);
    }

    public async Task<bool> Handle(int id)
    {
        try
        {
            await _healthMetricRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}