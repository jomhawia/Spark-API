

namespace e_commerce.Services.DTO
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }

    public class SubCategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public IFormFile? Image { get; set; }
        public int? CategoryId { get; set; }
    }

    public class SubCategoryGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
