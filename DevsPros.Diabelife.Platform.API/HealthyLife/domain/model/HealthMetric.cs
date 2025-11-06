using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

public class HealthMetric : BaseEntity
{
    public int HeartRate { get; set; }
    public double Glucose { get; set; }
    public double Weight { get; set; }
    public string BloodPressure { get; set; } = string.Empty;

    public HealthMetric() { }

    public HealthMetric(int heartRate, double glucose, double weight, string bloodPressure)
    {
        HeartRate = heartRate;
        Glucose = glucose;
        Weight = weight;
        BloodPressure = bloodPressure;
    }

    public void UpdateHealthData(int heartRate, double glucose, double weight, string bloodPressure)
    {
        HeartRate = heartRate;
        Glucose = glucose;
        Weight = weight;
        BloodPressure = bloodPressure;
        UpdatedAt = DateTime.UtcNow;
    }
}