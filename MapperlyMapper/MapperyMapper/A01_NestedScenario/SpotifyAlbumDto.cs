using System.Text.Json.Serialization;

namespace MapperlyMapper.A01_NestedScenario
{
    public class SpotifyAlbumDto
    {
        public string AlbumType { get; set; }

        public ArtistDto[] Artists { get; set; }

        public string[] AvailableMarkets { get; set; }

        public CopyrightDto[] Copyrights { get; set; }

        public ExternalIdsDto ExternalIds { get; set; }

        public ExternalUrlsDto ExternalUrls { get; set; }

        public object[] Genres { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public ImageDto[] Images { get; set; }

        public string Name { get; set; }

        public long Popularity { get; set; }

        public string ReleaseDate { get; set; }

        public string ReleaseDatePrecision { get; set; }

        public TracksDto Tracks { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }
    }
}