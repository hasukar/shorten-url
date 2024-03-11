using FluentValidation;

namespace UrlShortenerService.Application.Url.Commands;
public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        _ = RuleFor(v => v.Url)
          .NotEmpty()
          .WithMessage("Url is required.")
          .Must(ValidUrl)
          .WithMessage("The URL is not valid.");
    }

    private bool ValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
