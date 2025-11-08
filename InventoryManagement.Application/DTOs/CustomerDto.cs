namespace InventoryManagement.Application.DTOs
{
    public class CustomerCreateDto
    {
        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CustomerUpdateDto
    {
        public int Id { get; set; }

        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class CustomerListDto
    {
        public int Id { get; set; }

        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
