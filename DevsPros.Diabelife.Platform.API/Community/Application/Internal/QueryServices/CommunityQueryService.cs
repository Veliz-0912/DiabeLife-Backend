using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;
using CommunityPost = DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates.CommunityPost;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;


public class CommunityQueryService(
    ICommunityPostRepository postRepository,
    ICommentRepository commentRepository)
    : ICommunityQueryService
{
    public async Task<IEnumerable<CommunityPost>> HandleAsync(GetAllPostsQuery query)
        => await postRepository.ListAsync();

    public async Task<CommunityPost?> HandleAsync(GetPostByIdQuery query)
        => await postRepository.FindByIdAsync(query.PostId);

    public async Task<IEnumerable<CommunityPost>> HandleAsync(GetPostsByAuthorIdQuery query)
        => await postRepository.FindByAuthorIdAsync(query.AuthorId);

    public async Task<IEnumerable<Comment>> HandleAsync(GetCommentsByPostIdQuery query)
        => await commentRepository.FindByPostIdAsync(query.PostId);

    public async Task<IEnumerable<CommunityPost>> HandleAsync(SearchPostsByContentQuery query)
        => await postRepository.SearchByContentAsync(query.Content);
}