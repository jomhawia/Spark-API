namespace e_commerce.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string Role { get; set; }  
        public bool EmailConfirmed { get; set; } = false;

        public string? EmailOtpCode { get; set; }
        public DateTime? EmailOtpExpiresAt { get; set; }
        public DateTime? EmailOtpSentAt { get; set; }

        // ✅ حماية إضافية (Recommended)
        public int EmailOtpAttempts { get; set; } = 0;
        public DateTime? EmailOtpLockedUntil { get; set; }


        public string? ResetOtpCode { get; set; }
        public DateTime? ResetOtpExpiresAt { get; set; }
        public DateTime? ResetOtpSentAt { get; set; }
        public int ResetOtpAttempts { get; set; }
        public DateTime? ResetOtpLockedUntil { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // العلاقة مع العنوان
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }

}
