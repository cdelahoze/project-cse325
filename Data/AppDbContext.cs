using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
    public DbSet<FoodRegister> FoodRegisters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Clave compuesta para la tabla intermedia Food <-> Nutrient
        modelBuilder.Entity<FoodNutrient>()
            .HasKey(fn => new { fn.FoodId, fn.NutrientId });

        modelBuilder.Entity<FoodNutrient>()
            .HasOne(fn => fn.Food)
            .WithMany(f => f.FoodNutrients)
            .HasForeignKey(fn => fn.FoodId);

        modelBuilder.Entity<FoodNutrient>()
            .HasOne(fn => fn.Nutrient)
            .WithMany(n => n.FoodNutrients)
            .HasForeignKey(fn => fn.NutrientId);
    }
}
