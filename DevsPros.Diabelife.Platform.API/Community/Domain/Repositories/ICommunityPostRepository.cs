using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;

public interface ICommunityPostRepository
{
    Task<IEnumerable<CommunityPost>> ListAsync();
    Task<CommunityPost?> FindByIdAsync(Guid postId);
    Task AddAsync(CommunityPost post);
    void Remove(CommunityPost post);
    Task SaveChangesAsync();
}