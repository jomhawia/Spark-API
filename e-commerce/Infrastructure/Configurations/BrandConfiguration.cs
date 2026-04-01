using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {

            builder.ToTable("brands");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(255);

            builder.Property(x => x.NameAr)
                .HasColumnName("name_ar")
                .HasMaxLength(255);

            builder.Property(x => x.Logo)
                .HasColumnName("logo")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.NameAr).IsUnique();

            // إضافة التاريخ
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
