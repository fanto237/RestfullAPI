using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Villa> Villas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>()
            .Property(e => e.CreatedDate)
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
        modelBuilder.Entity<Villa>()
            .Property(e => e.UpdatedDate)
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Chateau d'eau",
                Details =
                    "Cras ut nunc at leo vehicula gravida sit amet sed odio. Suspendisse placerat porta urna in elementum",
                Occupancy = 4,
                Surface = 300,
                Rate = 12,
                ImageUrl =
                    "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80",
                Amenity =
                    "Sed facilisis, nibh id aliquet ultrices, ante nisi egestas turpis, euismod ornare metus urna eu neque. Etiam scelerisque elit rutrum augue sollicitudin sollicitudin.",
                CreatedDate = DateTime.Now
            },
            new Villa
            {
                Id = 2,
                Name = "Mon Village",
                Details =
                    "Mauris eget eros velit. Sed non facilisis risus, non cursus diam. Aenean gravida ac ex vitae malesuada. Integer venenatis interdum odio ut sodales.",
                Occupancy = 4,
                Surface = 500,
                Rate = 12,
                ImageUrl =
                    "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80",
                Amenity =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ut tincidunt ipsum. Morbi ex diam, euismod et erat viverra, aliquam malesuada risus. Etiam viverra sed elit non tempor",
                CreatedDate = DateTime.Now
            }
        );
    }
}