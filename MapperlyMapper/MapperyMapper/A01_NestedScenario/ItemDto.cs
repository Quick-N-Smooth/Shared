namespace MapperlyMapper.A01_NestedScenario
{
    public class ItemDto
    {
        public ArtistDto[] Artists { get; set; }

        public string[] AvailableMarkets { get; set; }

        public long DiscNumber { get; set; }

        public long DurationMs { get; set; }

        public bool Explicit { get; set; }

        public ExternalUrlsDto ExternalUrls { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PreviewUrl { get; set; }

        public long TrackNumber { get; set; }

        public string? Type { get; set; }

        public string Uri { get; set; }
    }
}