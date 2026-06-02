using System;

namespace BlazorApp.Models
{
    public class Workout
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ExerciseType { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
}
