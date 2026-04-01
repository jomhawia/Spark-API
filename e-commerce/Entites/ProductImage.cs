namespace e_commerce.Entites
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
