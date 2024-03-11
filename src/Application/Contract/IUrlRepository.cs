using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Contract;
public interface IUrlRepository : IAsyncRepository<ShortenUrl>
{
    Task<ShortenUrl?> GetShortenUrlById(int id);
}
