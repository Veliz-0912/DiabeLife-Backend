using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;

public interface IRecommendationCommandService
{
    Task<Recommendation> Handle(CreateRecommendationCommand command);
    Task<Recommendation> Handle(UpdateRecommendationCommand command);
    Task<bool> Handle(int id);
}

public interface IRecommendationQueryService
{
    Task<IEnumerable<Recommendation>> Handle();
    Task<Recommendation?> Handle(int id);
}