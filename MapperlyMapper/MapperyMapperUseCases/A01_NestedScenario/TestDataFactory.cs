using Bogus;
using MapperlyMapper.A01_NestedScenario;

namespace MapperyMapperTests.A01_NestedScenario
{
    public static class TestDataFactory
    {
        public static SpotifyAlbum CreateSpotifyAlbum = new SpotifyAlbum
        {
            AlbumType = "album",
            Artists = FakeArtists(2),
            AvailableMarkets = AvailableMarkets,
            Copyrights = new[]
            {
            new Copyright
            {
                Text = "(P) 2000 Sony Music Entertainment Inc.",
                Type = "P"
            }
        },
            ExternalIds = new ExternalIds
            {
                Upc = RandomNumericString(16)
            },
            ExternalUrls = new ExternalUrls
            {
                Spotify = $"https://open.spotify.com/album/{RandomAlphaNumericString(20)}"
            },
            Genres = Array.Empty<object>(),
            Href = $"https://api.spotify.com/v1/albums/{RandomAlphaNumericString(14)}",
            Id = RandomAlphaNumericString(14),
            Images = FakeImages(5),
            Name = "Keep coding",
            Popularity = 69,
            ReleaseDate = (new Faker().Random.Number(1960, 2021)).ToString(),
            ReleaseDatePrecision = "year",
            Tracks = new Tracks
            {
                Href = $"https://api.spotify.com/v1/albums/{RandomAlphaNumericString(12)}/tracks?offset=0&limit=50",
                Items = FakeItems(13),
                Limit = 50,
                Offset = 0,
                Total = 13
            }
        };


        internal static Artist[] FakeArtists(int count)
        {
            var urlFaker = new Faker<ExternalUrls>()
                .RuleFor(x => x.Spotify, x => $"https://open.spotify.com/artists/{RandomAlphaNumericString(14)}");

            var artistFaker = new Faker<Artist>()
                .RuleFor(x => x.ExternalUrls, x => urlFaker)
                .RuleFor(x => x.Href, $"https://api.spotify.com/v1/artists/{RandomAlphaNumericString(14)}")
                .RuleFor(x => x.Id, x => RandomAlphaNumericString(10))
                .RuleFor(x => x.Name, x => x.Name.FullName())
                .RuleFor(x => x.Type, "artist")
                .RuleFor(x => x.Uri, $"spotify:artist:{RandomAlphaNumericString(10)}");


            return artistFaker.Generate(count).ToArray();
        }

        internal static Item[] FakeItems(int count)
        {
            var urlFaker = new Faker<ExternalUrls>()
                .RuleFor(x => x.Spotify, x => $"https://open.spotify.com/artists/{x.Random.AlphaNumeric(14)}");

            var artist = FakeArtists(1);

            var itemFaker = new Faker<Item>()
                .RuleFor(x => x.Artists, x => artist)
                .RuleFor(x => x.DiscNumber, x => 1)
                .RuleFor(x => x.DurationMs, x => x.Random.Number(1000, 5500))
                .RuleFor(x => x.Explicit, x => false)
                .RuleFor(x => x.Name, x => x.Random.Words(4))
                .RuleFor(x => x.TrackNumber, x => x.IndexFaker)
                .RuleFor(x => x.Type, x => "track")
                .RuleFor(x => x.ExternalUrls, x => urlFaker)
                .RuleFor(x => x.PreviewUrl, x => $"https://p.scdn.co/mp3-preview/{x.Random.AlphaNumeric(30)}")
                .RuleFor(x => x.Uri, x => $"spotify:track:{x.Random.AlphaNumeric(14)}")
                .RuleFor(x => x.AvailableMarkets, x => AvailableMarkets);

            return itemFaker.Generate(count).ToArray();
        }

        internal static Image[] FakeImages(int count)
        {
            var imageFaker = new Faker<Image>()
                .RuleFor(x => x.Height, x => x.Random.Number(60, 90))
                .RuleFor(x => x.Width, x => x.Random.Number(60, 90))
                .RuleFor(x => x.Url, x => $"https://i.scdn.co/image/{x.Random.AlphaNumeric(20)}");

            return imageFaker.Generate(count).ToArray();
        }

        private static string[] AvailableMarkets = new[]
        {
        "AD","AR","AT","AU","BE","BG","BO","BR","CA","CH","CL","CO","CR","CY","CZ","DE","DK","DO","EC","EE","ES","FI","FR","GB","GR","GT","HK","HN","HU","IE","IS","IT","LI","LT","LU","LV","MC","MT","MX","MY","NI","NL","NO","NZ","PA","PE","PH","PT","PY","RO","SE","SG","SI","SK","SV","TW","UY"
    };

        public static string RandomAlphaNumericString(int length)
        {
            return new Faker().Random.AlphaNumeric(length);
        }

        public static string RandomNumericString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}