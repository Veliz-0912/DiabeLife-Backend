using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Services;

public interface ICommunityQueryService
{
    Task<IEnumerable<CommunityPost>> HandleAsync();
    Task<CommunityPost?> HandleAsync(Guid postId);
}