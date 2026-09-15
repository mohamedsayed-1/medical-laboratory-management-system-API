using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class RequestedLabTestConfiguration : IEntityTypeConfiguration<RequestedLabTest>
    {
        public void Configure(EntityTypeBuilder<RequestedLabTest> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasColumnName("Status")
                .IsRequired();

            builder.HasOne(r => r.Appointment)
                .WithMany(a => a.RequestedLabTests)
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(r => r.LabTest)
                .WithMany(l => l.RequestedLabTests)
                .HasForeignKey(r => r.LabTestId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}