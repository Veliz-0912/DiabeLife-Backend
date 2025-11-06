namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

public record CreateHealthMetricCommand(int HeartRate, double Glucose, double Weight, string BloodPressure);
public record UpdateHealthMetricCommand(int Id, int HeartRate, double Glucose, double Weight, string BloodPressure);