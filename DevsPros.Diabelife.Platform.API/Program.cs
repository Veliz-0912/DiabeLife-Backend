using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
// Authentication imports
using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
// Reports imports
using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Reports.Infrastructure.Persistence.EFC.Repositories;
// JWT imports
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "DiabeLife API", 
        Version = "v1",
        Description = "API for managing diabetes health metrics, food data, recommendations, authentication and reports",
        Contact = new OpenApiContact
        {
            Name = "DevsPros Team",
            Email = "devspros@diabelife.com"
        }
    });
    c.EnableAnnotations();
    
    // Add JWT authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlifyFrontend", policy =>
    {
        policy.WithOrigins("https://diabelife-frontend.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    
    options.AddPolicy("AllowDevelopment", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=diabelife;Uid=root;Pwd=password;";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);
});

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "your-secret-key-here-make-it-longer-than-32-characters-for-security";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "DiabeLifeAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "DiabeLifeClient";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Register Repositories
builder.Services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IFoodDataRepository, FoodDataRepository>();
// Authentication & Reports Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

// Register Command Services
builder.Services.AddScoped<IHealthMetricCommandService, HealthMetricCommandService>();
builder.Services.AddScoped<IRecommendationCommandService, RecommendationCommandService>();
builder.Services.AddScoped<IFoodDataCommandService, FoodDataCommandService>();
// Authentication & Reports Command Services
builder.Services.AddScoped<IAuthCommandService, AuthCommandService>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();

// Register Query Services
builder.Services.AddScoped<IHealthMetricQueryService, HealthMetricQueryService>();
builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();
builder.Services.AddScoped<IFoodDataQueryService, FoodDataQueryService>();
// Authentication & Reports Query Services
builder.Services.AddScoped<IAuthQueryService, AuthQueryService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiabeLife API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI the default page
    });
    app.UseCors("AllowDevelopment");
}
else
{
    app.UseCors("AllowNetlifyFrontend");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();