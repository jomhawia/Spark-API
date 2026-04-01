
namespace e_commerce.Services.DTO
{
    public class CartCreateDto
    {
        public int UserId { get; set; }
        public decimal TotelPrice { get; set; } // إذا بدك يكون 0 عند الإنشاء
    }

    public class CartUpdateDto
    {
        public int? UserId { get; set; }
        public decimal? TotelPrice { get; set; }
    }

    public class CartGetDto
    {
        public int Id { get; set; }
        public decimal TotelPrice { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
