using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

public class Comment
{
    public Guid Id { get; private set; }
    public AuthorId AuthorId { get; private set; }
    public Content Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Comment(AuthorId authorId, Content content)
    {
        Id = Guid.NewGuid();
        AuthorId = authorId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}