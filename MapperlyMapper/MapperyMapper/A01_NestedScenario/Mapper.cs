using Riok.Mapperly.Abstractions;
using Riok.Mapperly.Abstractions.ReferenceHandling;

namespace MapperlyMapper.A01_NestedScenario
{
    //[Mapper]
    [Mapper(UseReferenceHandling = true)]
    public partial class SpotifyAlbumMapper
    {
        public partial SpotifyAlbumDto SpotifyAlbumToSpotifyAlbumDto(SpotifyAlbum album);

        [MapProperty(nameof(Copyright.Text), nameof(CopyrightDto.CopyrightText))]
        public partial CopyrightDto MapToCopyrightDto(Copyright model);

    }
}
