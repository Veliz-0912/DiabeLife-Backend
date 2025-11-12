using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
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
    
    // Appointment DbSets
    public DbSet<AppointmentEntity> Appointments { get; set; }
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

        // Appointment Entity Configuration
        builder.Entity<AppointmentEntity>(entity =>
        {
            entity.ToTable("appointments");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(a => a.AppointmentDate).HasColumnName("appointment_date").IsRequired();
            entity.Property(a => a.Doctor).HasColumnName("doctor").HasMaxLength(200).IsRequired();
            entity.Property(a => a.Patient).HasColumnName("patient").HasMaxLength(200).IsRequired();
            entity.Property(a => a.AppointmentType).HasColumnName("appointment_type").HasMaxLength(100).IsRequired();
            entity.Property(a => a.Status).HasColumnName("status").HasMaxLength(50).IsRequired().HasDefaultValue("Scheduled");
            entity.Property(a => a.Notes).HasColumnName("notes").HasMaxLength(1000);
            entity.Property(a => a.Location).HasColumnName("location").HasMaxLength(300).IsRequired();
            entity.Property(a => a.Duration).HasColumnName("duration").IsRequired();
            entity.Property(a => a.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(a => a.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        builder.UseSnakeCaseNamingConvention();
    }
}