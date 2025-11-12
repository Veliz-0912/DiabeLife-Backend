using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Implementation of the unit of work pattern using Entity Framework Core.
/// </summary>
/// <param name="context">The database context.</param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// <summary>
    ///     Asynchronously saves all changes made in this unit of work.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}