namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

public record CreateRecommendationCommand(string Text);
public record UpdateRecommendationCommand(int Id, string Text);