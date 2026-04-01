using e_commerce.Entites;

namespace e_commerce.Services.DTO
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public OrderStatus? Status { get; set; } // إذا بدك تبدأ Pending خليها null
    }

    public class OrderUpdateDto
    {
        public int? UserId { get; set; }
        public decimal? Total { get; set; }
        public OrderStatus? Status { get; set; }
    }

    public class OrderStatusUpdateDto
    {
        public OrderStatus Status { get; set; }
    }

    public class OrderGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
