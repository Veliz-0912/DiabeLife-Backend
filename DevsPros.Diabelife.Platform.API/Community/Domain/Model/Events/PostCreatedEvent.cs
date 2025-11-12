using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;

public class PostCreatedEvent(string content, int authorId) : IEvent
{
    public string Content { get; } = content;
    public int AuthorId { get; } = authorId;
}