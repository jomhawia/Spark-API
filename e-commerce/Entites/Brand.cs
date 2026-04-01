namespace e_commerce.Entites
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set;  }

        public string Logo { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
