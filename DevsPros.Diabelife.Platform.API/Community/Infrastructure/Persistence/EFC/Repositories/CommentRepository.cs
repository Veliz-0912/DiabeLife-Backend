using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Community.Infrastructure.Persistence.EFC.Repositories;

public class CommentRepository(AppDbContext context)
    : BaseRepository<Comment, Guid>(context), ICommentRepository
{
    public async Task<IEnumerable<Comment>> FindByPostIdAsync(Guid postId)
    {
        return await Context.Set<Comment>()
            .Where(c => c.PostId.Value == postId)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await Context.Set<Comment>().AddAsync(comment);
    }
}