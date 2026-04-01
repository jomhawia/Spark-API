using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {

            builder.ToTable("cart_items");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(x=>x.UnitPrice)
                .HasColumnName("unit_price")
                .IsRequired();

            builder.Property(x=>x.CartId)
                .HasColumnName("cart_id")
                .IsRequired();

            builder.Property(x=>x.ProductVariantId)
                .HasColumnName("product_variant_id")
                .IsRequired();

            builder.HasOne(x=>x.Cart)
                .WithMany(x=>x.Items)
                .HasForeignKey(x=>x.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=>x.ProductVariant)
                .WithMany(x=>x.CartItems)
                .HasForeignKey(x=>x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
           .HasColumnName("created_at")
           .HasDefaultValueSql("CURRENT_TIMESTAMP")
           .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.HasIndex(x => new { x.CartId, x.ProductVariantId }).IsUnique();


        }
    }
}
