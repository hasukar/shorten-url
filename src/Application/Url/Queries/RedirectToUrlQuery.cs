using MediatR;

namespace UrlShortenerService.Application.Url.Queries;

public record RedirectToUrlQuery : IRequest<string>
{
    public string Id { get; init; } = default!;
}

