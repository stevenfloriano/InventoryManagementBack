using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagement.Domain.Entities
{
    public class SaleDetail
    {
        public int Id { get; set; }


        [Required]
        public int SaleId { get; set; }

        [JsonIgnore]
        public Sale? Sale { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Value { get; set; }

        [NotMapped]
        public decimal Subtotal => Quantity * Value;
    }
}
