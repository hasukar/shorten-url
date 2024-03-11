using Api.Endpoints.Url;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Url.Commands;
public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, CreateShortUrlResponse>
{
    private readonly IHashids _hashids;
    private readonly IUrlRepository _repository;

    public CreateShortUrlCommandHandler(
        IHashids hashids,
        IUrlRepository repository)
    {
        _hashids = hashids;
        _repository = repository;
    }

    public async Task<CreateShortUrlResponse> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        var shortenUrlEntity = new ShortenUrl { OriginalUrl = request.Url };

        var createdShortenUrlEntity = await _repository.AddAsync(shortenUrlEntity);

        var baseUrl = "https://localhost:7072";
        var hashCode = _hashids.EncodeLong(createdShortenUrlEntity.Id);
        var redirectPath = "v1/api/redirect";

        var shortenUrl = new Uri($"{baseUrl}/{redirectPath}/{hashCode}", UriKind.RelativeOrAbsolute);

        var response = new CreateShortUrlResponse
        {
            ShortenedUrl = shortenUrl.ToString(),
            OriginalUrl = request.Url
        };

        return response;
    }
}
