using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using CommunityPost = DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates.CommunityPost;

namespace DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // DbSets Community
    public DbSet<CommunityPost> CommunityPosts => Set<CommunityPost>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add created/updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // CommunityPost <-> Comments (1:N)
        builder.Entity<CommunityPost>()
            .HasMany(p => p.Comments)
            .WithOne() // no hay propiedad de navegación al post en Comment
            .HasForeignKey(c => c.PostId);

        // ValueObject conversions for CommunityPost
        builder.Entity<CommunityPost>()
            .Property(p => p.Id)
            .HasConversion(v => v.Value, v => new CommunityPostId(v))
            .ValueGeneratedNever();

        builder.Entity<CommunityPost>()
            .Property(p => p.AuthorId)
            .HasConversion(v => v.Value, v => new AuthorId(v));

        builder.Entity<CommunityPost>()
            .Property(p => p.Content)
            .HasConversion(v => v.Value, v => new Content(v));

        builder.Entity<CommunityPost>()
            .Property(p => p.ImageUrl)
            .HasConversion(v => v == null ? null : v.Value, v => v == null ? null : new ImageUrl(v));

        // ValueObject conversions for Comment
        builder.Entity<Comment>()
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Entity<Comment>()
            .Property(c => c.AuthorId)
            .HasConversion(v => v.Value, v => new AuthorId(v));

        builder.Entity<Comment>()
            .Property(c => c.Content)
            .HasConversion(v => v.Value, v => new Content(v));

        builder.Entity<Comment>()
            .Property(c => c.PostId)
            .HasConversion(v => v.Value, v => new CommunityPostId(v));

        // Naming convention
        builder.UseSnakeCaseNamingConvention();
    }
}
