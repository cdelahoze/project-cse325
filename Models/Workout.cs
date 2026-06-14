using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Workout
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Exercise type is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Exercise type must be between 2 and 100 characters.")]
        public string ExerciseType { get; set; } = string.Empty;

        [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes.")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(300, ErrorMessage = "Notes cannot be longer than 300 characters.")]
        public string? Notes { get; set; }
    }
}