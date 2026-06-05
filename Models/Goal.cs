using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    /// <summary>
    /// Represents a fitness or nutrition goal for a user.
    /// </summary>
    public class Goal
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? TargetDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
