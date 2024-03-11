using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Contract;
public interface ICreateShortUrlRepository
{
    Task<ShortenUrl> CreateShortUrl(ShortenUrl shortenUrlEntity, CancellationToken cancellationToken);
}
