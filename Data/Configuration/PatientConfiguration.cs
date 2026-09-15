using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(11) // Egyptian phone numbers without the global code(+20) are 11 digits
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.MaritalStatus)
                .HasConversion<string>()
                .IsRequired(false);

            builder.HasIndex(x => x.PhoneNumber).IsUnique();
        }
    }
}