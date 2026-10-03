using System.Security.Claims;
using Medical_Laboratory_Management_System.Constants;
using Medical_Laboratory_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Medical_Laboratory_Management_System.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var config = service.GetRequiredService<IConfiguration>();

            foreach (var role in new[] { Roles.Receptionist, Roles.Admin, Roles.Technician })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            if (await userManager.FindByNameAsync(config["SeedAdmin:UserName"]!) is null)
            {
                var newAcc = new ApplicationUser();
                newAcc.UserName = config["SeedAdmin:UserName"];
                newAcc.Email = config["SeedAdmin:Email"];
                newAcc.PhoneNumber = config["SeedAdmin:PhoneNumber"];
                var result = await userManager.CreateAsync(newAcc, config["SeedAdmin:Password"]!);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAcc, Roles.Admin);
                    await userManager.AddClaimAsync(newAcc, new Claim(ClaimTypes.NameIdentifier, newAcc.Id));
                    await userManager.AddClaimAsync(newAcc, new Claim(ClaimTypes.Name, newAcc.UserName!));
                }
            }
        }
    }
}
