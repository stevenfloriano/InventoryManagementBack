using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string? SKU { get; set; }

        [MaxLength(150)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }= string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
}
