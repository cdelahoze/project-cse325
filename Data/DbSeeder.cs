using System.Text.Json;
using BlazorApp.Models;

namespace BlazorApp.Data;

public static class DbSeeder
{
    // Las tres variantes con que USDA registra la energía; todas se normalizan a "Energy"
    private static readonly HashSet<string> EnergyNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Energy",
        "Energy (Atwater General Factors)",
        "Energy (Atwater Specific Factors)",
    };

    // Lista de nutrientes que nos interesan (nombres exactos del JSON de USDA)
    private static readonly HashSet<string> NutrientesDeInteres = new(StringComparer.OrdinalIgnoreCase)
    {
        // Macronutrientes
        "Protein",
        "Carbohydrate, by difference",
        "Total lipid (fat)",
        "Energy",
        "Fiber, total dietary",

        // Grasas por tipo
        "Fatty acids, total saturated",
        "Fatty acids, total monounsaturated",
        "Fatty acids, total polyunsaturated",
        "Fatty acids, total trans",
        "Cholesterol",

        // Omega-3
        "PUFA 18:3 n-3 c,c,c (ALA)",   // Ácido alfa-linolénico
        "PUFA 20:5 n-3 (EPA)",           // Ácido eicosapentaenoico
        "PUFA 22:6 n-3 (DHA)",           // Ácido docosahexaenoico

        // Omega-6
        "PUFA 18:2 n-6 c,c",             // Ácido linoleico

        // Omega-9 (ácido oleico - principal MUFA)
        "MUFA 18:1 c",

        // Minerales
        "Calcium, Ca",
        "Iron, Fe",
        "Magnesium, Mg",
        "Phosphorus, P",
        "Potassium, K",
        "Sodium, Na",
        "Zinc, Zn",
        "Copper, Cu",
        "Manganese, Mn",
        "Selenium, Se",

        // Vitaminas
        "Vitamin C, total ascorbic acid",
        "Vitamin A, RAE",
        "Vitamin D (D2 + D3), International Units",
        "Vitamin E (alpha-tocopherol)",
        "Vitamin K (phylloquinone)",
        "Thiamin",
        "Riboflavin",
        "Niacin",
        "Pantothenic acid",
        "Vitamin B-6",
        "Folate, total",
        "Vitamin B-12",
        "Choline, total",
    };

    public static void Seed(AppDbContext context)
    {
        // Si ya hay datos, no volver a sembrar
        if (context.Foods.Any()) return;

        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "foundationDownload.json");
        if (!File.Exists(jsonPath))
            jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "foundationDownload.json");

        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"[DbSeeder] Archivo JSON no encontrado en: {jsonPath}");
            return;
        }

        Console.WriteLine("[DbSeeder] Leyendo y procesando foundationDownload.json...");

        var json = File.ReadAllText(jsonPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var root = JsonSerializer.Deserialize<JsonRoot>(json, options);

        if (root?.FoundationFoods == null)
        {
            Console.WriteLine("[DbSeeder] No se pudo deserializar el JSON.");
            return;
        }

        // Catálogo de nutrientes en memoria para no duplicarlos
        var nutrientesCache = new Dictionary<string, Nutrient>(StringComparer.OrdinalIgnoreCase);

        foreach (var foodRaw in root.FoundationFoods)
        {
            if (string.IsNullOrWhiteSpace(foodRaw.Description)) continue;

            var food = new Food { Name = foodRaw.Description };
            var nutrientesEnEsteAlimento = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var fn in foodRaw.FoodNutrients ?? [])
            {
                var nName = fn.Nutrient?.Name;
                if (string.IsNullOrWhiteSpace(nName)) continue;

                // Normalizar las tres variantes de energía de USDA a un único nombre canónico
                if (EnergyNames.Contains(nName))
                    nName = "Energy";

                if (!NutrientesDeInteres.Contains(nName)) continue;

                // Para "Energy": normalizar todo a kcal.
                // Si la unidad es kJ, convertir (1 kcal = 4.184 kJ) y tratar como "Energy".
                // Si la unidad ya es kcal, usar directamente.
                // Se ignoran otras unidades de energía.
                double amount = fn.Amount;
                if (nName.Equals("Energy", StringComparison.OrdinalIgnoreCase))
                {
                    var unit = fn.Nutrient?.UnitName ?? string.Empty;
                    if (unit.Equals("kJ", StringComparison.OrdinalIgnoreCase))
                        amount = fn.Amount / 4.184;  // convertir kJ → kcal
                    else if (!unit.Equals("kcal", StringComparison.OrdinalIgnoreCase))
                        continue;  // ignorar IU u otras unidades de energía
                }

                // Evitar nutrientes duplicados en el mismo alimento
                if (!nutrientesEnEsteAlimento.Add(nName)) continue;

                // Obtener o crear el nutriente en el catálogo
                if (!nutrientesCache.TryGetValue(nName, out var nutriente))
                {
                    // Para Energy siempre almacenamos la unidad como kcal (ya convertimos arriba)
                    var unit = nName.Equals("Energy", StringComparison.OrdinalIgnoreCase)
                        ? "kcal"
                        : fn.Nutrient?.UnitName ?? "";
                    nutriente = new Nutrient
                    {
                        Name = nName,
                        Unit = unit
                    };
                    nutrientesCache[nName] = nutriente;
                    context.Nutrients.Add(nutriente);
                }

                food.FoodNutrients.Add(new FoodNutrient
                {
                    Nutrient = nutriente,
                    Amount = amount
                });
            }

            context.Foods.Add(food);
        }

        context.SaveChanges();
        Console.WriteLine($"[DbSeeder] Completado: {context.Foods.Count()} alimentos, {context.Nutrients.Count()} nutrientes.");
    }

    // --- DTOs privados para deserializar el JSON de USDA ---
    private class JsonRoot
    {
        public List<JsonFood>? FoundationFoods { get; set; }
    }

    private class JsonFood
    {
        public string? Description { get; set; }
        public List<JsonFoodNutrient>? FoodNutrients { get; set; }
    }

    private class JsonFoodNutrient
    {
        public JsonNutrient? Nutrient { get; set; }
        public double Amount { get; set; }
    }

    private class JsonNutrient
    {
        public string? Name { get; set; }
        public string? UnitName { get; set; }
    }
}
