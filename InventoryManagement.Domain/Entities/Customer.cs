using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string? Identification { get; set; }

        [MaxLength(50)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(150)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
}
