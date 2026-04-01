using e_commerce.Entites;
using e_commerce.Infrastructure.Repositories.@interface;
using e_commerce.Services.Auth;
using e_commerce.Services.DTO;
using e_commerce.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace e_commerce.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwt;

        public AuthController(IUserRepository userRepo, IEmailService emailService, IJwtService jwt)
        {
            _userRepo = userRepo;
            _emailService = emailService;
            _jwt = jwt;
        }

        private static string GenerateOtp(int digits = 6)
        {
            var max = (int)Math.Pow(10, digits);
            var code = RandomNumberGenerator.GetInt32(0, max);
            return code.ToString(new string('0', digits));
        }

        private static string EmailOtpHtml(string otp) => $@"
            <div style='font-family:Arial,sans-serif;line-height:1.6'>
                <h2>Email Verification</h2>
                <p>Your OTP code is:</p>
                <div style='font-size:28px;font-weight:bold;letter-spacing:4px'>{otp}</div>
                <p>This code expires in <b>10 minutes</b>.</p>
                <p>If you didn’t request this, ignore this email.</p>
            </div>";

        private static string ResetOtpHtml(string otp) => $@"
            <div style='font-family:Arial,sans-serif;line-height:1.6'>
                <h2>Password Reset</h2>
                <p>Your OTP code is:</p>
                <div style='font-size:28px;font-weight:bold;letter-spacing:4px'>{otp}</div>
                <p>This code expires in <b>10 minutes</b>.</p>
                <p>If you didn’t request this, ignore this email.</p>
            </div>";

        // POST: /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();

            if (await _userRepo.EmailExists(email))
                return Conflict(new { message = "Email already exists" });

            var user = new User
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = email,
                PhoneNumber = dto.PhoneNumber.Trim(),

                EmailConfirmed = false,
                Role = "User",

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, dto.Password);

            var otp = GenerateOtp(6);
            user.EmailOtpCode = otp;
            user.EmailOtpExpiresAt = DateTime.UtcNow.AddMinutes(10);
            user.EmailOtpSentAt = DateTime.UtcNow;
            user.EmailOtpAttempts = 0;
            user.EmailOtpLockedUntil = null;

            try
            {
                await _emailService.SendAsync(email, "Email Verification Code", EmailOtpHtml(otp));
                await _userRepo.Add(user);

                return StatusCode(201, new
                {
                    message = "User created. Please confirm your email using the OTP code sent to your email."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Could not create user", error = ex.ToString() });
            }
        }

        // POST: /api/auth/confirm-email
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();
            var code = dto.Code.Trim();

            var user = await _userRepo.GetByEmail(email);
            if (user == null) return NotFound(new { message = "User not found" });

            if (user.EmailConfirmed)
                return Ok(new { message = "Email already confirmed" });

            if (user.EmailOtpLockedUntil.HasValue && user.EmailOtpLockedUntil.Value > DateTime.UtcNow)
                return BadRequest(new { message = "Too many attempts. Try again later." });

            if (string.IsNullOrEmpty(user.EmailOtpCode) || !user.EmailOtpExpiresAt.HasValue)
                return BadRequest(new { message = "No OTP request found. Please resend confirmation." });

            if (user.EmailOtpExpiresAt.Value < DateTime.UtcNow)
                return BadRequest(new { message = "OTP expired. Please resend confirmation." });

            if (!string.Equals(user.EmailOtpCode, code, StringComparison.Ordinal))
            {
                user.EmailOtpAttempts += 1;

                if (user.EmailOtpAttempts >= 5)
                {
                    user.EmailOtpLockedUntil = DateTime.UtcNow.AddMinutes(10);
                    user.EmailOtpAttempts = 0;
                }

                user.UpdatedAt = DateTime.UtcNow;
                await _userRepo.Update(user);

                return BadRequest(new { message = "Invalid code" });
            }

            user.EmailConfirmed = true;
            user.EmailOtpCode = null;
            user.EmailOtpExpiresAt = null;
            user.EmailOtpSentAt = null;
            user.EmailOtpAttempts = 0;
            user.EmailOtpLockedUntil = null;

            user.UpdatedAt = DateTime.UtcNow;
            await _userRepo.Update(user);

            return Ok(new { message = "Email confirmed successfully" });
        }

        // POST: /api/auth/resend-confirmation
        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmation([FromBody] ResendConfirmationDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();
            var user = await _userRepo.GetByEmail(email);

            if (user == null)
                return Ok(new { message = "If the email exists, a code was sent." });

            if (user.EmailConfirmed)
                return Ok(new { message = "Email already confirmed" });

            if (user.EmailOtpSentAt.HasValue && user.EmailOtpSentAt.Value.AddSeconds(60) > DateTime.UtcNow)
                return BadRequest(new { message = "Please wait before requesting a new code." });

            var otp = GenerateOtp(6);

            user.EmailOtpCode = otp;
            user.EmailOtpExpiresAt = DateTime.UtcNow.AddMinutes(10);
            user.EmailOtpSentAt = DateTime.UtcNow;
            user.EmailOtpAttempts = 0;
            user.EmailOtpLockedUntil = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepo.Update(user);

            await _emailService.SendAsync(user.Email, "Email Verification Code", EmailOtpHtml(otp));

            return Ok(new { message = "Confirmation code resent to your email." });
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();
            var user = await _userRepo.GetByEmail(email);

            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            if (!user.EmailConfirmed)
                return BadRequest(new { message = "Please confirm your email first." });

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwt.CreateToken(user);

            return Ok(new
            {
                message = "Login successful",
                token,
                tokenType = "Bearer",
                user = new { user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber }
            });
        }

        // POST: /api/auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();
            var user = await _userRepo.GetByEmail(email);

            if (user == null)
                return Ok(new { message = "If the email exists, a code was sent." });

            if (user.ResetOtpLockedUntil.HasValue && user.ResetOtpLockedUntil.Value > DateTime.UtcNow)
                return BadRequest(new { message = "Too many attempts. Try again later." });

            if (user.ResetOtpSentAt.HasValue && user.ResetOtpSentAt.Value.AddSeconds(60) > DateTime.UtcNow)
                return BadRequest(new { message = "Please wait before requesting a new code." });

            var otp = GenerateOtp(6);

            user.ResetOtpCode = otp;
            user.ResetOtpExpiresAt = DateTime.UtcNow.AddMinutes(10);
            user.ResetOtpSentAt = DateTime.UtcNow;
            user.ResetOtpAttempts = 0;
            user.ResetOtpLockedUntil = null;

            user.UpdatedAt = DateTime.UtcNow;
            await _userRepo.Update(user);

            await _emailService.SendAsync(user.Email, "Password Reset Code", ResetOtpHtml(otp));

            return Ok(new { message = "If the email exists, a code was sent." });
        }

        // POST: /api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var email = dto.Email.Trim().ToLower();
            var code = dto.Code.Trim();

            var user = await _userRepo.GetByEmail(email);
            if (user == null) return NotFound(new { message = "User not found" });

            if (user.ResetOtpLockedUntil.HasValue && user.ResetOtpLockedUntil.Value > DateTime.UtcNow)
                return BadRequest(new { message = "Too many attempts. Try again later." });

            if (string.IsNullOrEmpty(user.ResetOtpCode) || !user.ResetOtpExpiresAt.HasValue)
                return BadRequest(new { message = "No reset request found. Please request a new code." });

            if (user.ResetOtpExpiresAt.Value < DateTime.UtcNow)
                return BadRequest(new { message = "OTP expired. Please request a new code." });

            if (!string.Equals(user.ResetOtpCode, code, StringComparison.Ordinal))
            {
                user.ResetOtpAttempts += 1;

                if (user.ResetOtpAttempts >= 5)
                {
                    user.ResetOtpLockedUntil = DateTime.UtcNow.AddMinutes(10);
                    user.ResetOtpAttempts = 0;
                }

                user.UpdatedAt = DateTime.UtcNow;
                await _userRepo.Update(user);

                return BadRequest(new { message = "Invalid code" });
            }

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, dto.NewPassword);

            user.ResetOtpCode = null;
            user.ResetOtpExpiresAt = null;
            user.ResetOtpSentAt = null;
            user.ResetOtpAttempts = 0;
            user.ResetOtpLockedUntil = null;

            user.UpdatedAt = DateTime.UtcNow;
            await _userRepo.Update(user);

            return Ok(new { message = "Password reset successfully" });
        }
    }
}