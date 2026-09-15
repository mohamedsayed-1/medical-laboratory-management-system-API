using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(e => e.Urgent)
                .HasDefaultValue(false);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}