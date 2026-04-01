namespace e_commerce.Entites
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // snapshot

        public int CartId { get; set; }

        public Cart Cart { get; set; } = null!;

        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
