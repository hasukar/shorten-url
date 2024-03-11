using System.Net;
using MediatR;
using UrlShortenerService.Api.Endpoints.Url.Requests;
using UrlShortenerService.Application.Url.Commands;
using IMapper = AutoMapper.IMapper;

namespace UrlShortenerService.Api.Endpoints.Url;

public class RedirectToUrlSummary : Summary<RedirectToUrlEndpoint>
{
    public RedirectToUrlSummary()
    {
        Summary = "Redirect to the original url from the short url";
        Description =
            "This endpoint will redirect to the original url from the short url. If the short url is not found, it will return a 404.";
        Response(StatusCodes.Status400BadRequest, "Request is invalid.");
        Response(StatusCodes.Status404NotFound, "No short url found.");
        Response(StatusCodes.Status500InternalServerError, "Internal server error.");
        Response(StatusCodes.Status301MovedPermanently, "Url is redirected.");
    }
}

public class RedirectToUrlEndpoint : BaseEndpoint<RedirectToUrlRequest>
{
    public RedirectToUrlEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    public override void Configure()
    {
        base.Configure();
        Get("v1/api/redirect/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("UrlManagement")
        );
        Summary(new RedirectToUrlSummary());
    }

    public override async Task HandleAsync(RedirectToUrlRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new RedirectToUrlCommand
            {
                Id = req.Id
            },
            ct
        );
        await SendRedirectAsync(result, false, ct);
    }
}
