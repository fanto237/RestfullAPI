using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mapper;

public class Mapper : IMapper
{
    public Villa Map<T>(VillaDto source) where T : Villa, new()
    {
        return Map(source, new T());
    }

    protected virtual Villa Map(VillaDto source, Villa destination)
    {        
        if(source.Id != 0)
            destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Occupancy = source.Occupancy;
        destination.Surface = source.Surface;
        destination.Details = source.Details;
        destination.Rate = source.Rate;
        destination.ImageUrl = source.ImageUrl;
        destination.Amenity = source.Amenity;
        destination.Details = source.Details;
        destination.CreatedDate = DateTime.Now;
        return destination;
    }
    
    public VillaDto Map<T>(Villa source) where T : VillaDto, new()
    {
        return Map(source, new T());
    }
    
    protected virtual VillaDto Map(Villa source, VillaDto destination)
    {
        if(source.Id != 0)
            destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Occupancy = source.Occupancy;
        destination.Surface = source.Surface;
        destination.Details = source.Details;
        destination.Rate = source.Rate;
        destination.ImageUrl = source.ImageUrl;
        destination.Amenity = source.Amenity;
        destination.Details = source.Details;
        return destination;
    }
}