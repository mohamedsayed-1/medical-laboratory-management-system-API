using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Medical_Laboratory_Management_System.Constants;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Medical_Laboratory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Role != Roles.Admin &&
                registerDTO.Role != Roles.Receptionist &&
                registerDTO.Role != Roles.Technician)
            {
                return BadRequest("Please, enter a valid role");
            }

            var newAcc = new ApplicationUser();
            newAcc.UserName = registerDTO.UserName;
            newAcc.Email = registerDTO.Email;
            newAcc.PhoneNumber = registerDTO.PhoneNumber;
            var result = await userManager.CreateAsync(newAcc, registerDTO.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(newAcc, registerDTO.Role);
                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(newAcc);
                    return BadRequest();
                }
                await userManager.AddClaimAsync(newAcc, new Claim(ClaimTypes.NameIdentifier, newAcc.Id));
                await userManager.AddClaimAsync(newAcc, new Claim(ClaimTypes.Name, newAcc.UserName));
                return Ok();
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.UserName);
            if (user is not null)
            {
                var password = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: true);
                if (password.Succeeded)
                {
                    var issuer = config["JWT:Issuer"];
                    var audience = config["JWT:Audience"];
                    var keyString = config["JWT:Key"];
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));
                    var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>();

                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var userClaims = await userManager.GetClaimsAsync(user);
                    claims.AddRange(userClaims);
                    var myToken = new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        claims: claims,
                        signingCredentials: signingCredential,
                        expires: DateTime.UtcNow.AddHours(1)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(myToken),
                        expires = myToken.ValidTo
                    });
                }
                return Unauthorized("User Name or Password are not correct");
            }
            return Unauthorized("User Name or Password are not correct");
        }
    }
}
