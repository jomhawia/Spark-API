
namespace e_commerce.Services.DTO
{
    public class CartItemCreateDto
    {
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
    }

    public class CartItemUpdateDto
    {
        public int? CartId { get; set; }
        public int? ProductVariantId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }

    public class CartItemGetDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
