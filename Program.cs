
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MLMSDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
            builder.Services.AddScoped<IPatientServices, PatientServices>();
            builder.Services.AddScoped<ILabTestServices, LabTestServices>();
            builder.Services.AddScoped<IRequestedLabTestServices, RequestedLabTestServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
