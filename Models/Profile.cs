using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(30, 500, ErrorMessage = "Initial weight must be between 30 and 500 kg.")]
        public decimal InitialWeightKg { get; set; }

        [Range(100, 250, ErrorMessage = "Height must be between 100 and 250 cm.")]
        public int HeightCm { get; set; }

        [Range(10, 120, ErrorMessage = "Age must be between 10 and 120.")]
        public int? Age { get; set; }

        [StringLength(80)]
        public string MainGoal { get; set; } = "Improve fitness and nutrition";

        [StringLength(80)]
        public string ActivityLevel { get; set; } = "Moderate";

        [StringLength(300)]
        public string Notes { get; set; } = string.Empty;
    }
}