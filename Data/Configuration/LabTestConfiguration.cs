using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}