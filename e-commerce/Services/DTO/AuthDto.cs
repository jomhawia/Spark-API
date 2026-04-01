using System.ComponentModel.DataAnnotations;

namespace e_commerce.Services.DTO
{
    public class RegisterDto
    {

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

    }
    public class ConfirmEmailDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(20)]
        public string Code { get; set; }
    }

    public class ResendConfirmationDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; }
    }

    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(20)]
        public string Code { get; set; }

        [Required, MinLength(8), MaxLength(100)]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }


}
