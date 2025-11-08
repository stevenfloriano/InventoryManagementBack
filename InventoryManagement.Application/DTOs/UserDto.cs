namespace InventoryManagement.Application.DTOs
{
    public class UserCreateDto
    {
        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }

        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class UserListDto
    {   
        public int Id { get; set; }

        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class UserLoginDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
