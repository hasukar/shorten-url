using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Exceptions;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Application.Contract;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Url.Commands;

public record RedirectToUrlCommand : IRequest<string>
{
    public string Id { get; init; } = default!;
}

public class RedirectToUrlCommandValidator : AbstractValidator<RedirectToUrlCommand>
{
    public RedirectToUrlCommandValidator()
    {
        _ = RuleFor(v => v.Id)
          .NotEmpty()
          .WithMessage("Id is required.");
    }
}

public class RedirectToUrlCommandHandler : IRequestHandler<RedirectToUrlCommand, string>
{
    private readonly IHashids _hashids;
    private readonly IRedirectToUrlRepository _repository;

    public RedirectToUrlCommandHandler(IHashids hashids, IRedirectToUrlRepository repository)
    {
        _hashids = hashids;
        _repository = repository;
    }

    public async Task<string> Handle(RedirectToUrlCommand request, CancellationToken cancellationToken)
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
