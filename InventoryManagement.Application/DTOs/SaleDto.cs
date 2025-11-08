using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.DTOs
{
    public class CreateSaleDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Note { get; set; } = string.Empty;

        [Required]
        public List<CreateSaleDetailDTO> SaleDetails { get; set; } = new();
    }

    public class UpdateSaleDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Note { get; set; } = string.Empty;

        [Required]
        public List<UpdateSaleDetailDTO> SaleDetails { get; set; } = new();
    }

    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal Total { get; set; }
        public string Note { get; set; } = string.Empty;
        public List<SaleDetailDTO> SaleDetails { get; set; } = new();
    }

    public class CreateSaleDetailDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Value { get; set; }
    }

    public class UpdateSaleDetailDTO : CreateSaleDetailDTO
    {
        public int? Id { get; set; }
    }

    public class SaleDetailDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductListDto? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Subtotal => Quantity * Value;
    }
}
