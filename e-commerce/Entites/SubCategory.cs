namespace e_commerce.Entites
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}

