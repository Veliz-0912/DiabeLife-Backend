namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

public record CreateFoodDataCommand(string Food, DateTime? Timestamp = null);
public record UpdateFoodDataCommand(int Id, string Food);