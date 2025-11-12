using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

public partial class CommunityPost
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommunityPost"/> class with the specified author, content, and optional image.
    /// </summary>
    /// <param name="authorId">The author ID.</param>
    /// <param name="content">The content of the post.</param>
    /// <param name="imageUrl">Optional image URL.</param>
    public CommunityPost(AuthorId authorId, Content content, ImageUrl? imageUrl = null) : this()
    {
        Id = new CommunityPostId();
        AuthorId = authorId;
        Content = content;
        ImageUrl = imageUrl;
        CreatedAt = DateTime.UtcNow;
        Likes = 0;
    }
public CommunityPost(){}
    /// <summary>
    /// Initializes a new instance of the <see cref="CommunityPost"/> class from a create post command.
    /// </summary>
    /// <param name="command">The create post command.</param>
    public CommunityPost(CreatePostCommand command) 
        : this(new AuthorId(command.AuthorId), new Content(command.Content), 
               command.ImageUrl != null ? new ImageUrl(command.ImageUrl) : null)
    {
    }

    /// <summary>
    /// Gets the unique identifier of the post.
    /// </summary>
    public CommunityPostId Id { get; private set; }

    /// <summary>
    /// Gets the author identifier of the post.
    /// </summary>
    public AuthorId AuthorId { get; private set; }

    /// <summary>
    /// Gets the content of the post.
    /// </summary>
    public Content Content { get; private set; }

    /// <summary>
    /// Gets the image URL of the post, if any.
    /// </summary>
    public ImageUrl? ImageUrl { get; private set; }

    /// <summary>
    /// Gets the number of likes of the post.
    /// </summary>
    public int Likes { get; private set; }

    /// <summary>
    /// Gets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    /// <summary>
    /// Adds a comment to this post.
    /// </summary>
    /// <param name="command">The add comment command.</param>
    public void AddComment(AddCommentCommand command)
    {
        var comment = new Comment(new AuthorId(command.AuthorId), new Content(command.Content));
        _comments.Add(comment);
    }

    /// <summary>
    /// Adds a like to this post.
    /// </summary>
    public void AddLike()
    {
        Likes++;
    }

    /// <summary>
    /// Removes a like from this post.
    /// </summary>
    public void RemoveLike()
    {
        if (Likes > 0) Likes--;
    }

    /// <summary>
    /// Deletes the post if the author matches.
    /// </summary>
    public bool CanBeDeletedBy(int authorId)
    {
        return AuthorId.Value == authorId;
    }
}