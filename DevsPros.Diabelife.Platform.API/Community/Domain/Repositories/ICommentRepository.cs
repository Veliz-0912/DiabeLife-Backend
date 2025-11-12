using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> FindByPostIdAsync(Guid postId);
    Task AddAsync(Comment comment);
}