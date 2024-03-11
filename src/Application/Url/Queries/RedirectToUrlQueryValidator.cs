using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace UrlShortenerService.Application.Url.Queries;
public class RedirectToUrlCommandValidator : AbstractValidator<RedirectToUrlQuery>
{
    public RedirectToUrlCommandValidator()
    {
        _ = RuleFor(v => v.Id)
          .NotEmpty()
          .WithMessage("Id is required.");
    }
}
