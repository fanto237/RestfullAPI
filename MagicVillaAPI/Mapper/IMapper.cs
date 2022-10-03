using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mapper;

public interface IMapper
{
    Villa Map<T>(VillaDto source) where T : Villa, new();
    VillaDto Map<T>(Villa source) where T : VillaDto, new();
}