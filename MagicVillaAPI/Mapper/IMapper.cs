using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mapper;

public interface IMyMapper
{
    VillaUpdateDto Map<T>(Villa source) where T : VillaUpdateDto, new();
    // VillaDto Map<T>(Villa source) where T : VillaDto, new();
    Villa Map<T>(VillaCreateDto source) where T : Villa, new();
    Villa Map<T>(VillaUpdateDto source) where T : Villa, new(); 
}