using e_commerce.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PhoneEvaluationConfiguration : IEntityTypeConfiguration<PhoneEvaluation>
    {
        public void Configure(EntityTypeBuilder<PhoneEvaluation> builder)
        {
            builder.ToTable("PhoneEvaluations");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(p => p.PhoneBrand)
                .IsRequired()
                .HasMaxLength(50); 


            builder.Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();


            builder.Property(p => p.PercentageOfUsed)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfScreen)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfBackScreen)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfBattery)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfCamera)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfOpen)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfOutScrren) 
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfBody)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PercentageOfBiometrics)
                .HasColumnType("float")
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasIndex(p => p.Name)
           .IsUnique();


            builder.HasCheckConstraint("CHK_PercentageOfScreen", "[PercentageOfScreen] >= 0 AND [PercentageOfScreen] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfBackScreen", "[PercentageOfBackScreen] >= 0 AND [PercentageOfBackScreen] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfBattery", "[PercentageOfBattery] >= 0 AND [PercentageOfBattery] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfCamera", "[PercentageOfCamera] >= 0 AND [PercentageOfCamera] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfOpen", "[PercentageOfOpen] >= 0 AND [PercentageOfOpen] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfOutScrren", "[PercentageOfOutScrren] >= 0 AND [PercentageOfOutScrren] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfBody", "[PercentageOfBody] >= 0 AND [PercentageOfBody] <= 100");
            builder.HasCheckConstraint("CHK_PercentageOfBiometrics", "[PercentageOfBiometrics] >= 0 AND [PercentageOfBiometrics] <= 100");
        }
    }

}
