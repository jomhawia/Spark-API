using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .HasDefaultValue(false)
                .IsRequired();

            // ---------------------------
            // Email OTP (Confirm Email)
            // ---------------------------
            builder.Property(x => x.EmailOtpCode)
                .HasColumnName("email_otp_code")
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.EmailOtpExpiresAt)
                .HasColumnName("email_otp_expires_at")
                .IsRequired(false);

            builder.Property(x => x.EmailOtpSentAt)
                .HasColumnName("email_otp_sent_at")
                .IsRequired(false);

            builder.Property(x => x.EmailOtpAttempts)
                .HasColumnName("email_otp_attempts")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.EmailOtpLockedUntil)
                .HasColumnName("email_otp_locked_until")
                .IsRequired(false);

            // ---------------------------
            // Reset Password OTP
            // ---------------------------
            builder.Property(x => x.ResetOtpCode)
                .HasColumnName("reset_otp_code")
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.ResetOtpExpiresAt)
                .HasColumnName("reset_otp_expires_at")
                .IsRequired(false);

            builder.Property(x => x.ResetOtpSentAt)
                .HasColumnName("reset_otp_sent_at")
                .IsRequired(false);

            builder.Property(x => x.ResetOtpAttempts)
                .HasColumnName("reset_otp_attempts")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.ResetOtpLockedUntil)
                .HasColumnName("reset_otp_locked_until")
                .IsRequired(false);

            // ---------------------------
            // Role (الـ Role الخاص بالمستخدم)
            // ---------------------------
            builder.Property(x => x.Role)
                .HasColumnName("role")
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("User"); // افتراضياً يكون "User" إذا لم يتم تحديده

            // timestamps
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
