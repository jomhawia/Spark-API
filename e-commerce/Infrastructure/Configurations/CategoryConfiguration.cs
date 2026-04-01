using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>

    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {


            builder.ToTable("categories");


            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.NameAr)
                   .HasColumnName("name_ar")
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Image)
                   .HasColumnName("image")
                   .HasMaxLength(500);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.NameAr).IsUnique();

            builder.HasMany(x => x.SubCategories)
               .WithOne()
               .HasForeignKey(sc => sc.CategoryId) // assuming SubCategory has CategoryId as FK
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

        }
    }
}
