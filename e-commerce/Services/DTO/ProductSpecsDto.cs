using System;

namespace e_commerce.Services.DTO
{
    public class ProductSpecsCreateDto
    {
        public string SpecKey { get; set; }
        public string SpecValue { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductSpecsUpdateDto
    {
        public string? SpecKey { get; set; }
        public string? SpecValue { get; set; }
        public int? ProductId { get; set; }
    }

    public class ProductSpecsGetDto
    {
        public int Id { get; set; }
        public string? SpecKey { get; set; }
        public string? SpecValue { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
