using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;
using UrlShortenerService.Infrastructure.Common;
using UrlShortenerService.Infrastructure.Persistence;

namespace UrlShortenerService.Infrastructure.Repositories;
public class UrlRepository : RepositoryBase<ShortenUrl, ApplicationDbContext>, IUrlRepository
{
    public UrlRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ShortenUrl?> GetShortenUrlById(int id)
    {
        var shortenUrlEntity = await _dbContext.ShortenUrls.FirstOrDefaultAsync(x => x.Id == id);

        return shortenUrlEntity;
    }

    /*public async Task<ShortenUrl> AddAsync(ShortenUrl shortenUrlEntity, CancellationToken cancellationToken)
    {
        _ = await _dbContext.ShortenUrls.AddAsync(shortenUrlEntity, cancellationToken);
        _ = await _dbContext.SaveChangesAsync(cancellationToken);

        return shortenUrlEntity;
    }*/
}
