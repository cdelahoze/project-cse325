namespace BlazorApp.Models;

// Registro diario del usuario
public class FoodRegister
{
    public int Id { get; set; }

    public int FoodId { get; set; }
    public Food Food { get; set; } = null!;

    public DateTime Date { get; set; } = DateTime.Today;
    public double Grams { get; set; } = 100;
    public string MealType { get; set; } = string.Empty; // Desayuno, Almuerzo, Cena, Snack
}
