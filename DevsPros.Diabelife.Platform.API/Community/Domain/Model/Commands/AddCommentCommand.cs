namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
public record AddCommentCommand(Guid PostId, int AuthorId, string Content);
