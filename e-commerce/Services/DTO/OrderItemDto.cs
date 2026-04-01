using System;

namespace e_commerce.Services.DTO
{
    public class OrderItemCreateDto
    {
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // snapshot
    }

    public class OrderItemUpdateDto
    {
        public int? OrderId { get; set; }
        public int? ProductVariantId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }

    public class OrderItemGetDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
