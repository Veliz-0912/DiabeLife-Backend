using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;

public class PostLikedEvent(int postId, int newLikeCount) : IEvent
{
    public int PostId { get; } = postId;
    public int NewLikeCount { get; } = newLikeCount;
}