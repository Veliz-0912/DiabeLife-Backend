using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Services;

public interface ICommunityCommandService
{
    Task<CommunityPost> Handle(CreatePostCommand command);
    Task Handle(DeletePostCommand command);
    Task Handle(AddCommentCommand command);
    Task Handle(AddLikeCommand command);
}