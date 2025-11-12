using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Community.Infrastructure.Persistence.EFC.Repositories;

public class CommunityPostRepository : BaseRepository<CommunityPost, Guid>, ICommunityPostRepository
{
    public CommunityPostRepository(AppDbContext context) : base(context) { }

    // Buscar por Id usando el ValueObject
    public async Task<CommunityPost?> FindByIdAsync(CommunityPostId postId)
    {
        return await Context.Set<CommunityPost>()
            .FirstOrDefaultAsync(p => p.Id.Value == postId.Value);
    }

    public async Task<IEnumerable<CommunityPost>> FindByAuthorIdAsync(AuthorId authorId)
    {
        return await Context.Set<CommunityPost>()
            .Where(p => p.AuthorId.Value == authorId.Value)
            .ToListAsync();
    }

    public async Task<IEnumerable<CommunityPost>> SearchByContentAsync(string content)
    {
        return await Context.Set<CommunityPost>()
            .Where(p => p.Content.Value.Contains(content))
            .ToListAsync();
    }
}