using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI.Mapper;

public class Mapper : IMyMapper
{
    public Villa Map<T>(VillaDto source) where T : Villa, new()
    {
        return Map(source, new T());
    }

    public Villa Map<T>(VillaCreateDto source) where T : Villa, new()
    {
        return Map(source, new T());
    }

    private Villa Map(VillaCreateDto source, Villa destination)
    {
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

    public Villa Map<T>(VillaUpdateDto source) where T : Villa, new()
    {
        return Map(source, new T());
    }

    private Villa Map(VillaUpdateDto source, Villa destination)
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
        destination.UpdatedDate = DateTime.Now;
        return destination;
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
    
    public VillaUpdateDto Map<T>(Villa source) where T : VillaUpdateDto, new()
    {
        return Map(source, new T());
    }
    
    protected virtual VillaUpdateDto Map(Villa source, VillaUpdateDto destination)
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