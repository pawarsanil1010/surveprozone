using System.ComponentModel.DataAnnotations;

namespace SurveProzone.Models
{
    public class ContactForm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Service { get; set; } = "";
    }
}