using System.Text.Json.Serialization;

namespace e_commerce.Entites
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Image {  get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();


    }
}
