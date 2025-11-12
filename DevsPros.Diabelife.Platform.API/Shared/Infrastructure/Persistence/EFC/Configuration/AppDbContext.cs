using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;
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
    
    // Authentication & Reports DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }
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

        // User Entity Configuration
        builder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(u => u.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash").HasMaxLength(500).IsRequired();
            entity.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            // Unique constraint for email
            entity.HasIndex(u => u.Email).IsUnique();
        });

        // Report Entity Configuration
        builder.Entity<Report>(entity =>
        {
            entity.ToTable("reports");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(r => r.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(r => r.Date).HasColumnName("date").IsRequired();
            entity.Property(r => r.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            entity.Property(r => r.Data).HasColumnName("data").IsRequired();
            entity.Property(r => r.Selected).HasColumnName("selected").IsRequired();
            entity.Property(r => r.Shared).HasColumnName("shared").IsRequired();
            entity.Property(r => r.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(r => r.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            // Foreign key relationship
            entity.HasOne(r => r.User)
                  .WithMany(u => u.Reports)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.UseSnakeCaseNamingConvention();
    }
}