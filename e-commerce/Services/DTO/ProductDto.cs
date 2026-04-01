using Microsoft.AspNetCore.Http;
using System;

namespace e_commerce.Services.DTO
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string Description { get; set; }
        public string DescriptionAr { get; set; }

        public IFormFile MainImage { get; set; }

        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
    }

    public class ProductUpdateDto
    {

        public string? Name { get; set; }
        public string? NameAr { get; set; }

        public string? Description { get; set; }
        public string? DescriptionAr { get; set; }

        public IFormFile? MainImage { get; set; }

        public int? SubCategoryId { get; set; }
        public int? BrandId { get; set; }
    }

    public class ProductGetDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? NameAr { get; set; }

        public string? Description { get; set; }
        public string? DescriptionAr { get; set; }

        public string MainImage { get; set; }

        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
