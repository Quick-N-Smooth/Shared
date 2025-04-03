using MapperlyMapper.A01_NestedScenario;

namespace MapperyMapperTests.A01_NestedScenario
{
    [TestFixture]
    public class MapperUseCase
    {
        [Test]
        public void Mapper_HappyFlow()
        {
            SpotifyAlbum album = TestDataFactory.CreateSpotifyAlbum;

            var mapper = new SpotifyAlbumMapper();

            var dto = mapper.SpotifyAlbumToSpotifyAlbumDto(album);

            Assert.That(dto is not null,
                "Cars must be set");

        }
    }
}
