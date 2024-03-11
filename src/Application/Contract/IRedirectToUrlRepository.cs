using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Contract;
public interface IRedirectToUrlRepository
{
    Task<ShortenUrl?> GetShortenUrlById(int id);
}
