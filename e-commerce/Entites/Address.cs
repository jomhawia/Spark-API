namespace e_commerce.Entites
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Details { get; set; } = null!;

        public bool IsDefault { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
