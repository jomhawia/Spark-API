using System.ComponentModel.DataAnnotations;

namespace e_commerce.Services.DTO
{
    public class AddressCreateDto
    {
      
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required, MinLength(2)]
        public string City { get; set; } = "";

        [Required]
        public string Area { get; set; } = "";

        [Required]
        public string Street { get; set; } = "";

        [Required]
        public string Details { get; set; } = "";

        public bool IsDefault { get; set; }
    }

    public class AddressUpdateDto
    {
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Street { get; set; }
        public string? Details { get; set; }
        public bool? IsDefault { get; set; }
        public int? UserId { get; set; }
    }

    public class AddressGetDto
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Street { get; set; }
        public string? Details { get; set; }
        public bool IsDefault { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SetDefaultAddressDto
    {
        public int UserId { get; set; }
    }
}
