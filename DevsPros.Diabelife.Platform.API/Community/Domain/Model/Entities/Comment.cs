using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

public class Comment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public AuthorId AuthorId { get; private set; }
    public Content Content { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    protected Comment() { }

    public Comment(AuthorId authorId, Content content)
    {
        AuthorId = authorId;
        Content = content;
    }
}