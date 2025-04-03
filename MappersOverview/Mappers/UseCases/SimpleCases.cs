using AutoMapper;
using Mappers;
using Mappers.Models;
using Mapster;

namespace Mappers.UseCases;


// This use case represents the simples usage of each mapper

public class SimpleCases
{
    private readonly SpotifyAlbumDto _spotifyAlbumDto = TestDataFactory.CreateSpotifyAlbumDto;

    public SpotifyAlbum AutoMapper()
    {
        //Automapper Configuration 
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SpotifyAlbumDto, SpotifyAlbum>();
            cfg.CreateMap<CopyrightDto, Copyright>();
            cfg.CreateMap<ArtistDto, Artist>();
            cfg.CreateMap<ExternalIdsDto, ExternalIds>();
            cfg.CreateMap<ExternalUrlsDto, ExternalUrls>();
            cfg.CreateMap<TracksDto, Tracks>();
            cfg.CreateMap<ImageDto, Image>();
            cfg.CreateMap<ItemDto, Item>();
            cfg.CreateMap<SpotifyAlbum, SpotifyAlbumDto>();
            cfg.CreateMap<Copyright, CopyrightDto>();
            cfg.CreateMap<Artist, ArtistDto>();
            cfg.CreateMap<ExternalIds, ExternalIdsDto>();
            cfg.CreateMap<ExternalUrls, ExternalUrlsDto>();
            cfg.CreateMap<Tracks, TracksDto>();
            cfg.CreateMap<Image, ImageDto>();
            cfg.CreateMap<Item, ItemDto>();
        });
        var autoMapper = mapperConfig.CreateMapper();
        return autoMapper.Map<SpotifyAlbum>(_spotifyAlbumDto);
    }

    public SpotifyAlbum TinyMapper()
    {
        //TinyMapper Configuration 
        Nelibur.ObjectMapper.TinyMapper.Bind<SpotifyAlbumDto, SpotifyAlbum>();
        Nelibur.ObjectMapper.TinyMapper.Bind<CopyrightDto, Copyright>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ArtistDto, Artist>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalIdsDto, ExternalIds>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalUrlsDto, ExternalUrls>();
        Nelibur.ObjectMapper.TinyMapper.Bind<TracksDto, Tracks>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ImageDto, Image>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ItemDto, Item>();
        Nelibur.ObjectMapper.TinyMapper.Bind<SpotifyAlbum, SpotifyAlbumDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<Copyright, CopyrightDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<Artist, ArtistDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalIds, ExternalIdsDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalUrls, ExternalUrlsDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<Tracks, TracksDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<Image, ImageDto>();
        Nelibur.ObjectMapper.TinyMapper.Bind<Item, ItemDto>();
        return Nelibur.ObjectMapper.TinyMapper.Map<SpotifyAlbum>(_spotifyAlbumDto);
    }

    public SpotifyAlbum Mapster()
    {
        return _spotifyAlbumDto.Adapt<SpotifyAlbum>();
    }

    public SpotifyAlbum Mapperly()
    {
        var mapperlyMapper = new MapperlyMapper();
        return mapperlyMapper.Map(_spotifyAlbumDto);
    }


    public SpotifyAlbum ManualMappingReference()
    {
        return _spotifyAlbumDto.ManualMapping();
    }
}
