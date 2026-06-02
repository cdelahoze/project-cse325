using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(30, 500, ErrorMessage = "Peso inicial inválido")] 
        public decimal InitialWeightKg { get; set; }

        [Range(100, 250, ErrorMessage = "Altura inválida")] 
        public int HeightCm { get; set; }

        [Range(10, 120, ErrorMessage = "Edad inválida")] 
        public int? Age { get; set; }
    }
}
