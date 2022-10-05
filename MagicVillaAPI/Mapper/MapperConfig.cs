using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Villa, VillaDto>();
        CreateMap<VillaCreateDto, Villa>().ReverseMap();
        CreateMap<VillaUpdateDto, Villa>().ReverseMap();
    }
}