using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto;

public class VillaDto
{
    public int Id { get; set; }
    [Required] [MaxLength(30)] public string Name { get; set; } = null!;
    public int Occupancy { get; set; }
    public int Surface { get; set; }
    public string Details { get; set; } = null!;
    public double Rate { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Amenity { get; set; } = null!;
}