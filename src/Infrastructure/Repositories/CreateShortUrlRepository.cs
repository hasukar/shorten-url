using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Infrastructure.Repositories;
public class CreateShortUrlRepository : ICreateShortUrlRepository
{
    private readonly IApplicationDbContext _context;
    public CreateShortUrlRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShortenUrl> CreateShortUrl(ShortenUrl shortenUrlEntity, CancellationToken cancellationToken)
    {
        _ = await _context.ShortenUrls.AddAsync(shortenUrlEntity, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return shortenUrlEntity;
    }
}
