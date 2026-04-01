
namespace e_commerce.Services.DTO
{
    public class ProductVariantCreateDto
    {
        public string VariantName { get; set; } = null!;
        public string SKU { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductVariantUpdateDto
    {
        public string? VariantName { get; set; }
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public int? ProductId { get; set; }
    }

    public class ProductVariantGetDto
    {
        public int Id { get; set; }
        public string? VariantName { get; set; }
        public string? SKU { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
