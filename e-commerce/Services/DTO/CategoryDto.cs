namespace e_commerce.Services.DTO
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public IFormFile Image { get; set; }
    }
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
