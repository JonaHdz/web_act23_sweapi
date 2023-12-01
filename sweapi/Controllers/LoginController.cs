using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sweapi.Data;
using sweapi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace swebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public LoginController(IdentityContext context, UserManager<CustomIdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Unauthorized("Acceso no autorizado.");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginViewModel login)
        {
            var usuario = await _userManager.FindByEmailAsync(login.Correo);

            if (usuario is null || !await _userManager.CheckPasswordAsync(usuario, login.Password))
            {
                return Unauthorized("Acceso no autorizado.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.GivenName, usuario.Nombre)
            };

            var roles = await _userManager.GetRolesAsync(usuario);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Ok(new
            {
                usuario.Id,
                usuario.Email,
                usuario.Nombre,
                rol = string.Join(",", roles),
                AccessToken = jwt
            });
        }
    }
}
