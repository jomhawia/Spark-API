namespace e_commerce.Entites
{
    public class ProductSpecs
    {
        public int Id { get; set; }

        public string SpecKey { get; set; }

        public string SpecValue { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
