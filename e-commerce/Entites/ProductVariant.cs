namespace e_commerce.Entites
{
    public class ProductVariant
    {

        public int Id { get; set; }

        public string VariantName { get; set; } = null!;

        public string SKU { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public ICollection<CartItem>? CartItems { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
