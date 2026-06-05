using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    /// <summary>
    /// Represents a meal entry with basic nutrition info.
    /// </summary>
    public class Meal
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Approximate calories for the meal.</summary>
        public int Calories { get; set; }

        public string? Notes { get; set; }
    }
}
