namespace MapperlyMapper.A01_NestedScenario
{
    public class SpotifyAlbum
    {
        public string AlbumType { get; set; }
        public Artist[] Artists { get; set; }
        public string[] AvailableMarkets { get; set; }
        public Copyright[] Copyrights { get; set; }
        public ExternalIds ExternalIds { get; set; }
        public ExternalUrls ExternalUrls { get; set; }
        public object[] Genres { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public Image[] Images { get; set; }
        public string Name { get; set; }
        public long Popularity { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseDatePrecision { get; set; }
        public Tracks Tracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}