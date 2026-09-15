using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class LabTestResultConfiguration : IEntityTypeConfiguration<LabTestResult>
    {
        public void Configure(EntityTypeBuilder<LabTestResult> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Value)
                .IsUnicode()
                .IsRequired();
            
            builder.Property(x => x.Notes)
                .IsUnicode()
                .IsRequired(false);

            builder.HasOne(x => x.RequestedLabTest)
                .WithOne(x => x.LabTestResult)
                .HasForeignKey<LabTestResult>(x => x.RequestedLabTestId)
                .IsRequired();
        }
    }
}