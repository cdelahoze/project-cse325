namespace BlazorApp.Models;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<FoodNutrient> FoodNutrients { get; set; } = new List<FoodNutrient>();
    public ICollection<FoodRegister> FoodRegisters { get; set; } = new List<FoodRegister>();
}
