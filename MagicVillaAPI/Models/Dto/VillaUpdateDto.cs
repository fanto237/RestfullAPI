using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto;

public class VillaUpdateDto
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(30)] public string Name { get; set; } = null!;

    public int Occupancy { get; set; }

    [Required] public int Surface { get; set; }

    [Required] public string Details { get; set; } = null!;

    [Required] public double Rate { get; set; }

    [Required] public string ImageUrl { get; set; } = null!;

    public string Amenity { get; set; } = null!;
}