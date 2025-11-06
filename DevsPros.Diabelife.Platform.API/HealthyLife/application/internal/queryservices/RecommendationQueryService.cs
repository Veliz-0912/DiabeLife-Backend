using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.QueryServices;

internal class RecommendationQueryService : IRecommendationQueryService
{
    private readonly IRecommendationRepository _recommendationRepository;

    public RecommendationQueryService(IRecommendationRepository recommendationRepository)
    {
        _recommendationRepository = recommendationRepository;
    }

    public async Task<IEnumerable<Recommendation>> Handle()
    {
        return await _recommendationRepository.GetAllAsync();
    }

    public async Task<Recommendation?> Handle(int id)
    {
        return await _recommendationRepository.GetByIdAsync(id);
    }
}