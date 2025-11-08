namespace InventoryManagement.Application.DTOs
{
    public class ProductCreateDto
    {
        public string SKU { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
    }

    public class ProductUpdateDto
    {
        public int Id { get; set; }

        public string? SKU { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class ProductListDto
    {
        public int Id { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }
    }
}
