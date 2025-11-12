namespace DevsPros.Diabelife.Platform.API.Community.Aggregates;

public partial class CommunityPost
{
    public int Id { get; }
    public int AuthorId { get;set;  }
    public string Content { get;set; }
    public string Username { get; set; }
    
    public CommunityPost(int authotId,string content,string username)
    {
        Id = authotId;
        Content = content;
    }
}