
namespace e_commerce.Services.DTO
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }   
        public string PhoneNumber { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // ملاحظة: ما بحط Password هون (افضل endpoint لحاله)
        public int? CartId { get; set; }
    }

    public class UserGetDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // (اختياري) Endpoint لتغيير كلمة المرور
    public class UserChangePasswordDto
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
