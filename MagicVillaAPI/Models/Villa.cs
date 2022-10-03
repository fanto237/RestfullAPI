using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVillaAPI.Models;

public class Villa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int Occupancy { get; set; }
    public int Surface { get; set; }
    public string Details { get; set; } = null!;
    public double Rate { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Amenity { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}