using Microsoft.AspNetCore.Http;
using System;

namespace e_commerce.Services.DTO
{
    public class ProductImageCreateDto
    {
        public IFormFile Image { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductImageUpdateDto
    {
        public IFormFile? Image { get; set; }
        public int? ProductId { get; set; }
    }

    public class ProductImageGetDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
