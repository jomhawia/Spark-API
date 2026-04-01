using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {

            builder.ToTable("subcategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id )
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

            builder.Property(x => x.NameAr)
               .HasColumnName("name_ar")
               .HasMaxLength(255);


            builder.Property(x => x.Image)
                   .HasColumnName("image")
                   .HasMaxLength(500);


            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(c => c.SubCategories)   
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(x => x.CategoryId);

            // قيد فريد لأسم الفئة بالإنجليزي والعربي
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.NameAr).IsUnique();


        }
    }
}
