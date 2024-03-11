using UrlShortenerService.Domain.Common;

namespace UrlShortenerService.Domain.Entities;

/// <summary>
/// Url domain entity.
/// </summary>
public class ShortenUrl : BaseAuditableEntity
{
    #region constructors and destructors

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ShortenUrl() { }

    #endregion

    #region properties

    /// <summary>
    /// The original url.
    /// </summary>
    public string OriginalUrl { get; set; } = default!;

    #endregion
}
