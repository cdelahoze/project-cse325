using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(30, 500, ErrorMessage = "Initial weight is invalid")] 
        public decimal InitialWeightKg { get; set; }

        [Range(100, 250, ErrorMessage = "Height is invalid")] 
        public int HeightCm { get; set; }

        [Range(10, 120, ErrorMessage = "Age is invalid")] 
        public int? Age { get; set; }
    }
}
