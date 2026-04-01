namespace e_commerce.Entites
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotelPrice { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
