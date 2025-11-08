using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string? Identification { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Password { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
}
