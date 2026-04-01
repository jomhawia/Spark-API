using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.ToTable("order_items");

            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .IsRequired();

            builder.Property(x => x.LineTotal)
               .HasColumnName("line_total")
               .IsRequired();


            builder.Property(x => x.OrderId)
               .HasColumnName("order_id")
               .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x=>x.Items)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ProductVariantId)
               .HasColumnName("product_variant_id")
               .IsRequired();

            builder.HasOne(x=>x.ProductVariant)
                .WithMany(x=>x.OrderItems)
                .HasForeignKey(x=>x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.OrderId, x.ProductVariantId }).IsUnique();

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
