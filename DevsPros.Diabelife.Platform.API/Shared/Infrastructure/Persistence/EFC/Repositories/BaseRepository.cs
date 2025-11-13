using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public Task<TEntity?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    // Este es el cambio clave: recibe un ValueObject con propiedad Value
    public async Task<TEntity?> FindByIdAsync(TId id)
    {
        // EF necesita el valor primitivo de la clave
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}