using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;

public class CommentAddedEvent(int postId, int authorId, string text) : IEvent
{
    public int PostId { get; } = postId;
    public int AuthorId { get; } = authorId;
    public string Text { get; } = text;
}