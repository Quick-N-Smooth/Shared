using Mappers.Models;
using Riok.Mapperly.Abstractions;

namespace Mappers;

// NOTE: for Mapperly, you need to create a partial class for each mapping 

[Mapper]
public partial class MapperlyMapper
{
    public partial SpotifyAlbum Map(SpotifyAlbumDto spotifyAlbumDto);
}
