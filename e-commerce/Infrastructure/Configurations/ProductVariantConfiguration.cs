using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {

            builder.ToTable("product_variants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.VariantName)
                .HasColumnName("variant_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SKU)
              .HasColumnName("SKU")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(x => x.Price)
             .HasColumnName("price")
             .IsRequired();

            builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .IsRequired();

            builder.Property(x => x.ProductId)
             .HasColumnName("product_id")
             .IsRequired();

            builder.HasOne(x=>x.Product)
                .WithMany(x=>x.Variants)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SKU).IsUnique();

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
