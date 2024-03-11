using Api.Endpoints.Url;
using MediatR;

namespace UrlShortenerService.Application.Url.Commands;

public record CreateShortUrlCommand : IRequest<CreateShortUrlResponse>
{
    public string Url { get; init; } = default!;
}
