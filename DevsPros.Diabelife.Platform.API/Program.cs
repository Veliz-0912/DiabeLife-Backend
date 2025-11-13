using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.Community.Infrastructure.Interfaces.ASP.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers with kebab-case routes
builder.Services.AddControllers(options =>
    options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Database configuration
builder.AddDatabaseServices();

// Swagger / OpenAPI configuration
builder.AddOpenApiDocumentationServices();

// Register all bounded contexts (modular structure)
builder.AddSharedContextServices();
builder.AddCommunityContextServices();

// Mediator or CQRS configuration
builder.AddCortexConfigurationServices();

var app = builder.Build();

// Database assurance
app.UseDatabaseCreationAssurance();

// Middleware pipeline
app.UseOpenApiDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();