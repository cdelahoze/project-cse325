using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    /// <summary>
    /// Minimal user representation for the tracker.
    /// </summary>
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? DisplayName { get; set; }
    }
}
