using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // HealthyLife DbSets
    public DbSet<HealthMetric> HealthMetrics { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<FoodData> FoodData { get; set; }
    
    // Notifications DbSets
    public DbSet<Notification> Notifications { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // HealthMetrics Entity Configuration
        builder.Entity<HealthMetric>(entity =>
        {
            entity.ToTable("health_metrics");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(h => h.HeartRate).HasColumnName("heart_rate").IsRequired();
            entity.Property(h => h.Glucose).HasColumnName("glucose").IsRequired();
            entity.Property(h => h.Weight).HasColumnName("weight").IsRequired();
            entity.Property(h => h.BloodPressure).HasColumnName("blood_pressure").HasMaxLength(20).IsRequired();
            entity.Property(h => h.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(h => h.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        // Recommendations Entity Configuration
        builder.Entity<Recommendation>(entity =>
        {
            entity.ToTable("recommendations");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(r => r.Text).HasColumnName("text").HasMaxLength(500).IsRequired();
            entity.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(r => r.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        // FoodData Entity Configuration
        builder.Entity<FoodData>(entity =>
        {
            entity.ToTable("food_data");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(f => f.Food).HasColumnName("food").HasMaxLength(200).IsRequired();
            entity.Property(f => f.Timestamp).HasColumnName("timestamp").IsRequired();
            entity.Property(f => f.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(f => f.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        // Notifications Entity Configuration
        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(n => n.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            entity.Property(n => n.Message).HasColumnName("message").HasMaxLength(500).IsRequired();
            entity.Property(n => n.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
            entity.Property(n => n.UserId).HasColumnName("user_id").HasMaxLength(100).IsRequired();
            entity.Property(n => n.IsRead).HasColumnName("is_read").IsRequired();
            entity.Property(n => n.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(n => n.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        builder.UseSnakeCaseNamingConvention();
    }
}