namespace MapperlyMapper.A01_NestedScenario
{
    public class Tracks
    {
        public string Href { get; set; }
        public Item[] Items { get; set; }
        public long Limit { get; set; }
        public object Next { get; set; }
        public long Offset { get; set; }
        public object Previous { get; set; }
        public long Total { get; set; }
    }
}