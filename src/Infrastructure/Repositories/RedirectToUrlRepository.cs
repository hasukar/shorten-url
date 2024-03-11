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
public class RedirectToUrlRepository : IRedirectToUrlRepository
{
    private readonly IApplicationDbContext _context;

    public RedirectToUrlRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShortenUrl?> GetShortenUrlById(int id)
    {
        var shortenUrlEntity = await _context.ShortenUrls.FirstOrDefaultAsync(x => x.Id == id);

        return shortenUrlEntity;
    }
}
