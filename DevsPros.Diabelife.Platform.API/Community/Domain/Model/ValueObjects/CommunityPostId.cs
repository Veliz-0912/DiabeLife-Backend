namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record CommunityPostId(Guid Value)
{
    public CommunityPostId() : this(Guid.NewGuid()) { }
}