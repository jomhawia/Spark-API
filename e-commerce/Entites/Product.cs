namespace e_commerce.Entites
{
    public class Product
    {
         
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAr {  get; set; }

        public string Description { get; set; }

        public string DescriptionAr { get; set; }

        public string MainImage { get; set; }

        public int SubCategoryId { get; set; }

        public int BrandId { get; set; }
        public SubCategory SubCategory { get; set; } = null!;

        public Brand Brand { get; set; } = null!;

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ICollection<ProductSpecs> Specs { get; set; } = new List<ProductSpecs>();

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
