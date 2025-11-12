using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;

public interface ICommunityPostRepository
{
    Task<IEnumerable<CommunityPost>> ListAsync();
    Task<CommunityPost?> FindByIdAsync(Guid id);
    Task<IEnumerable<CommunityPost>> FindByAuthorIdAsync(Guid authorId);
    Task<IEnumerable<CommunityPost>> SearchByContentAsync(string content);
    Task AddAsync(CommunityPost post);
    void Remove(CommunityPost post);
}