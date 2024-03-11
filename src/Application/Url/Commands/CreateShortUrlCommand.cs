using Api.Endpoints.Url;
using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Url.Commands;

public record CreateShortUrlCommand : IRequest<CreateShortUrlResponse>
{
    public string Url { get; init; } = default!;
}

public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        _ = RuleFor(v => v.Url)
          .NotEmpty()
          .WithMessage("Url is required.")
          .Must(ValidUrl)
          .WithMessage("");
    }

    private bool ValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, CreateShortUrlResponse>
{
    private readonly IHashids _hashids;
    private readonly ICreateShortUrlRepository _repository;

    public CreateShortUrlCommandHandler(
        IHashids hashids,
        ICreateShortUrlRepository repository)
    {
        _hashids = hashids;
        _repository = repository;
    }

    public async Task<CreateShortUrlResponse> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        var shortenUrlEntity = new ShortenUrl { OriginalUrl = request.Url };

        var createdShortenUrlEntity = await _repository.CreateShortUrl(shortenUrlEntity, cancellationToken);

        var baseUrl = "https://localhost:7072";
        var hashCode = _hashids.EncodeLong(createdShortenUrlEntity.Id);

        var shortenUrl = new Uri($"{baseUrl}/{hashCode}", UriKind.RelativeOrAbsolute);

        var response = new CreateShortUrlResponse
        {
            ShortenedUrl = shortenUrl.ToString(),
            OriginalUrl = request.Url
        };

        return response;
    }
}
