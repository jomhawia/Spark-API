using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

            builder.Property(x => x.NameAr)
                .HasColumnName("name_ar")
                .HasMaxLength(255);


            builder.Property(x => x.Description)
                   .HasColumnName("description")
                   .HasMaxLength(1000);  // Adjust length if needed

            builder.Property(x => x.DescriptionAr)
                   .HasColumnName("description_ar")
                   .HasMaxLength(1000);  // Adjust length if needed

         
            builder.Property(x => x.MainImage)
                .HasColumnName("main_image")
                .HasMaxLength(255);

            builder.Property(x => x.SubCategoryId)
                .HasColumnName("subCategory_id")
                .IsRequired();

            builder.Property(x => x.BrandId)
               .HasColumnName("brand_id")
               .IsRequired();

            builder.HasOne(x => x.SubCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(x=>x.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Brand)
                .WithMany(c => c.Products)
                .HasForeignKey(x=> x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SubCategoryId);

            builder.HasIndex(x => x.BrandId);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.NameAr).IsUnique();

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
