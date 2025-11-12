namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

public record ImageUrl(string Url)
{
    public ImageUrl() : this(string.Empty) { }

    public static ImageUrl From(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return new ImageUrl(string.Empty);

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new ArgumentException("La URL de imagen no es válida.", nameof(url));

        return new ImageUrl(url);
    }

    public bool HasValue() => !string.IsNullOrEmpty(Url);
}
