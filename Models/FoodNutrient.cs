namespace BlazorApp.Models;

// Tabla intermedia: cantidad de cada nutriente por cada 100g de alimento
public class FoodNutrient
{
    public int FoodId { get; set; }
    public Food Food { get; set; } = null!;

    public int NutrientId { get; set; }
    public Nutrient Nutrient { get; set; } = null!;

    public double Amount { get; set; }
}
