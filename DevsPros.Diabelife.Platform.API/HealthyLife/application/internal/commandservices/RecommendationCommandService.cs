using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;

internal class RecommendationCommandService : IRecommendationCommandService
{
    private readonly IRecommendationRepository _recommendationRepository;

    public RecommendationCommandService(IRecommendationRepository recommendationRepository)
    {
        _recommendationRepository = recommendationRepository;
    }

    public async Task<Recommendation> Handle(CreateRecommendationCommand command)
    {
        var recommendation = new Recommendation(command.Text);
        return await _recommendationRepository.AddAsync(recommendation);
    }

    public async Task<Recommendation> Handle(UpdateRecommendationCommand command)
    {
        var existingRecommendation = await _recommendationRepository.GetByIdAsync(command.Id);
        if (existingRecommendation == null)
            throw new Exception($"Recommendation with id {command.Id} not found");

        existingRecommendation.UpdateText(command.Text);
        return await _recommendationRepository.UpdateAsync(existingRecommendation);
    }

    public async Task<bool> Handle(int id)
    {
        try
        {
            await _recommendationRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}