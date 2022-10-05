using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserRegisterController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<AuthResponse>> Registrar(UserCredential userCredential)
        {
            var user = new IdentityUser
            {
                UserName = userCredential.UserName,
                Email = userCredential.Email,
            };
            var result = await userManager.CreateAsync(user, userCredential.Password);

            if (result.Succeeded)
            {
                return GenerarToken(userCredential);
            }
            else
            {
                //return BadRequest();
                return BadRequest(result.Errors); //Fines Didácticos
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserCredential userCredential)
        {
            var result = await signInManager.PasswordSignInAsync(userCredential.UserName, userCredential.Password,isPersistent:false, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return GenerarToken(userCredential);
            }
            else
            {
                return BadRequest("Login failed");
            }
        }

        private AuthResponse GenerarToken(UserCredential userCredential)
        {
            var claims = new List<Claim>
            {
                new Claim("usuario",userCredential.UserName),
                new Claim("email", userCredential.Email),
                new Claim("test", "this is a test"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddDays(1);
            var securityToken = new JwtSecurityToken(issuer: null, claims: claims, expires: expiration, signingCredentials: credential);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };
        }

    }
}
