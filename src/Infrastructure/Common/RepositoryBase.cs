using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Domain.Common;

namespace UrlShortenerService.Infrastructure.Common;
public class RepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
        where TEntity : BaseAuditableEntity
        where TContext : DbContext
{
    protected readonly TContext _dbContext;

    public RepositoryBase(TContext dbContextBase)
    {
        _dbContext = dbContextBase;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        /*_dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();*/

        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
