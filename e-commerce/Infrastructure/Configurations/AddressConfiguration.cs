using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.City)
                .HasColumnName("city")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Area)
              .HasColumnName("area")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(x => x.Street)
              .HasColumnName("street")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(x => x.Details)
              .HasColumnName("details")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(x => x.IsDefault)
              .HasColumnName("is_default")
              .IsRequired();

            builder.Property(x => x.UserId)
              .HasColumnName("user_id")
              .IsRequired();

            // تعديل سلوك الحذف ليكون Restrict بدلاً من Cascade
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  // استخدام Restrict بدلاً من SetNull أو Cascade

            // قيد فريد على UserId و IsDefault لضمان أن يكون لكل مستخدم عنوان افتراضي واحد فقط
            builder.HasIndex(x => new { x.UserId, x.IsDefault }).IsUnique();

            // إضافة التواريخ إذا لزم الأمر
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        }
    }
}
