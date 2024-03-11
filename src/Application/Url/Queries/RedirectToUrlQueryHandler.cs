using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Exceptions;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Url.Queries;
public class RedirectToUrlQueryHandler : IRequestHandler<RedirectToUrlQuery, string>
{
    private readonly IHashids _hashids;
    private readonly IUrlRepository _repository;

    public RedirectToUrlQueryHandler(IHashids hashids, IUrlRepository repository)
    {
        _hashids = hashids;
        _repository = repository;
    }

    public async Task<string> Handle(RedirectToUrlQuery request, CancellationToken cancellationToken)
    {
        var shortenUrlEntityId = _hashids.DecodeSingle(request.Id);

        var shortenUrlEntity = await _repository.GetShortenUrlById(shortenUrlEntityId);

        if (shortenUrlEntity is null)
        {
            throw new NotFoundException(nameof(ShortenUrl), request.Id);
        }

        return shortenUrlEntity.OriginalUrl;
    }
}
