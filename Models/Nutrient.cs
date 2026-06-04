namespace BlazorApp.Models;

public class Nutrient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;

    public ICollection<FoodNutrient> FoodNutrients { get; set; } = new List<FoodNutrient>();
}
