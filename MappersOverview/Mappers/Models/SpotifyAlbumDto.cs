using System.Text.Json.Serialization;

namespace Mappers.Models;

public class SpotifyAlbumDto
{
    [JsonPropertyName("album_type")] public string AlbumType { get; set; }

    [JsonPropertyName("artists")] public ArtistDto[] Artists { get; set; }

    [JsonPropertyName("available_markets")]
    public string[] AvailableMarkets { get; set; }

    [JsonPropertyName("copyrights")] public CopyrightDto[] Copyrights { get; set; }

    [JsonPropertyName("external_ids")] public ExternalIdsDto ExternalIds { get; set; }

    [JsonPropertyName("external_urls")] public ExternalUrlsDto ExternalUrls { get; set; }

    [JsonPropertyName("genres")] public object[] Genres { get; set; }

    [JsonPropertyName("href")] public string Href { get; set; }

    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("images")] public ImageDto[] Images { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("popularity")] public long Popularity { get; set; }

    [JsonPropertyName("release_date")] public string ReleaseDate { get; set; }

    [JsonPropertyName("release_date_precision")]
    public string ReleaseDatePrecision { get; set; }

    [JsonPropertyName("tracks")] public TracksDto Tracks { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("uri")] public string Uri { get; set; }
}

public class TracksDto
{
    [JsonPropertyName("href")] public string Href { get; set; }

    [JsonPropertyName("items")] public ItemDto[] Items { get; set; }

    [JsonPropertyName("limit")] public long Limit { get; set; }

    [JsonPropertyName("next")] public object Next { get; set; }

    [JsonPropertyName("offset")] public long Offset { get; set; }

    [JsonPropertyName("previous")] public object Previous { get; set; }

    [JsonPropertyName("total")] public long Total { get; set; }
}

public class ItemDto
{
    [JsonPropertyName("artists")] public ArtistDto[] Artists { get; set; }

    [JsonPropertyName("available_markets")]
    public string[] AvailableMarkets { get; set; }

    [JsonPropertyName("disc_number")] public long DiscNumber { get; set; }

    [JsonPropertyName("duration_ms")] public long DurationMs { get; set; }

    [JsonPropertyName("explicit")] public bool Explicit { get; set; }

    [JsonPropertyName("external_urls")] public ExternalUrlsDto ExternalUrls { get; set; }

    [JsonPropertyName("href")] public string Href { get; set; }

    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("preview_url")] public string PreviewUrl { get; set; }

    [JsonPropertyName("track_number")] public long TrackNumber { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("uri")] public string Uri { get; set; }
}

public class ImageDto
{
    [JsonPropertyName("height")] public long Height { get; set; }

    [JsonPropertyName("url")] public string Url { get; set; }

    [JsonPropertyName("width")] public long Width { get; set; }
}

public class ExternalIdsDto
{
    [JsonPropertyName("upc")] public string Upc { get; set; }
}

public class CopyrightDto
{
    [JsonPropertyName("text")] public string Text { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }
}

public class ArtistDto
{
    [JsonPropertyName("external_urls")] public ExternalUrlsDto ExternalUrls { get; set; }

    [JsonPropertyName("href")] public string Href { get; set; }

    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("uri")] public string Uri { get; set; }
}

public class ExternalUrlsDto
{
    [JsonPropertyName("spotify")] public string Spotify { get; set; }
}
