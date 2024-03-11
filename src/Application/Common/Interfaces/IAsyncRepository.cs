using UrlShortenerService.Domain.Common;

namespace UrlShortenerService.Application.Common.Interfaces;
public interface IAsyncRepository<TEntity> where TEntity : BaseAuditableEntity
{
    Task<TEntity> AddAsync(TEntity entity);

}
