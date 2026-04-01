namespace e_commerce.Entites
{
    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }

    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; } 

        public User User { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
