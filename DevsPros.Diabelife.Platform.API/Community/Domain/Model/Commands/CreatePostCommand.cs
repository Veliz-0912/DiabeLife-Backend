namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

public record CreatePostCommand(int AuthorId, string Content, string? ImageUrl = null);
