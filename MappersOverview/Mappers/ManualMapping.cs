using Mappers.Models;

namespace Mappers;

public static class MappingGeneratorMapper
{

    public static SpotifyAlbum ManualMapping(this SpotifyAlbumDto spotifyAlbumDto)
    {
        var result = new SpotifyAlbum
        {
            AlbumType = spotifyAlbumDto.AlbumType,
            AvailableMarkets = spotifyAlbumDto.AvailableMarkets,
            ExternalIds = new ExternalIds
            {
                Upc = spotifyAlbumDto.ExternalIds.Upc
            },
            ExternalUrls = new ExternalUrls
            {
                Spotify = spotifyAlbumDto.ExternalUrls.Spotify
            },
            Genres = spotifyAlbumDto.Genres,
            Href = spotifyAlbumDto.Href,
            Id = spotifyAlbumDto.Id,
            Name = spotifyAlbumDto.Name,
            Popularity = spotifyAlbumDto.Popularity,
            ReleaseDate = spotifyAlbumDto.ReleaseDate,
            ReleaseDatePrecision = spotifyAlbumDto.ReleaseDatePrecision,
            Type = spotifyAlbumDto.Type,
            Uri = spotifyAlbumDto.Uri,
            Artists = new Artist[spotifyAlbumDto.Artists.Length]
        };

        for (var i = 0; i < spotifyAlbumDto.Artists.Length; i++)
        {
            var artistDto = spotifyAlbumDto.Artists[i];
            result.Artists[i] = new Artist
            {
                ExternalUrls = new ExternalUrls
                {
                    Spotify = artistDto.ExternalUrls.Spotify
                },
                Href = artistDto.Href,
                Id = artistDto.Id,
                Name = artistDto.Name,
                Type = artistDto.Type,
                Uri = artistDto.Uri
            };
        }

        result.Copyrights = new Copyright[spotifyAlbumDto.Copyrights.Length];
        for (var i = 0; i < spotifyAlbumDto.Copyrights.Length; i++)
        {
            var copyrightDto = spotifyAlbumDto.Copyrights[i];
            result.Copyrights[i] = new Copyright
            {
                Text = copyrightDto.Text,
                Type = copyrightDto.Type
            };
        }

        result.Images = new Image[spotifyAlbumDto.Images.Length];
        for (var i = 0; i < spotifyAlbumDto.Images.Length; i++)
        {
            var imageDto = spotifyAlbumDto.Images[i];
            result.Images[i] = new Image
            {
                Height = imageDto.Height,
                Url = imageDto.Url,
                Width = imageDto.Width
            };
        }

        result.Tracks = new Tracks
        {
            Href = spotifyAlbumDto.Tracks.Href,
            Limit = spotifyAlbumDto.Tracks.Limit,
            Next = spotifyAlbumDto.Tracks.Next,
            Offset = spotifyAlbumDto.Tracks.Offset,
            Previous = spotifyAlbumDto.Tracks.Previous,
            Total = spotifyAlbumDto.Tracks.Total,
            Items = new Item[spotifyAlbumDto.Tracks.Items.Length]
        };

        for (var i = 0; i < spotifyAlbumDto.Tracks.Items.Length; i++)
        {
            var itemDto = spotifyAlbumDto.Tracks.Items[i];
            result.Tracks.Items[i] = new Item();
            var item = result.Tracks.Items[i];
            item.AvailableMarkets = itemDto.AvailableMarkets;
            item.DiscNumber = itemDto.DiscNumber;
            item.DurationMs = itemDto.DurationMs;
            item.Explicit = itemDto.Explicit;
            item.ExternalUrls = new ExternalUrls
            {
                Spotify = itemDto.ExternalUrls.Spotify
            };
            item.Href = itemDto.Href;
            item.Id = itemDto.Id;
            item.Name = itemDto.Name;
            item.PreviewUrl = itemDto.PreviewUrl;
            item.TrackNumber = itemDto.TrackNumber;
            item.Type = itemDto.Type;
            item.Uri = itemDto.Uri;

            item.Artists = new Artist[itemDto.Artists.Length];
            for (var j = 0; j < itemDto.Artists.Length; j++)
            {
                var artistDto = itemDto.Artists[j];
                item.Artists[j] = new Artist
                {
                    ExternalUrls = new ExternalUrls
                    {
                        Spotify = artistDto.ExternalUrls.Spotify
                    },
                    Href = artistDto.Href,
                    Id = artistDto.Id,
                    Name = artistDto.Name,
                    Type = artistDto.Type,
                    Uri = artistDto.Uri
                };
            }
        }
        return result;
    }
}
