

namespace e_commerce.Services.DTO
{
    public class BrandCreateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public IFormFile Logo { get; set; }
    }

    public class BrandUpdateDto
    {
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public IFormFile? Logo { get; set; }
    }

    public class BrandGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string Logo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
