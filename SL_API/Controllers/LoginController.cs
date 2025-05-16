using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ML;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static DL.AppDbContext;

namespace SL_API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //[Route("Login")]
        //public IActionResult Login([FromBody] ML.Login login)
        //{
        //    ML.Result result = BL.Login.GetByEmailAndPassword(login.Email, login.Password, _context);

        //    if (result.Correct)
        //    {
        //        return Ok(result.Object);
        //    }
        //    else
        //    {
        //        return Unauthorized(result.ErrorMessage);
        //    }
        //}

        [HttpPost("login")]

        public IActionResult Login([FromBody] Login model)

        {

            var usuario = _context.Logins.FirstOrDefault(u => u.Email == model.Email);

            if (usuario == null)

                return Unauthorized("Usuario no encontrado");

            // Crear claims (información dentro del token)

            var claims = new[]

            {

            new Claim(ClaimTypes.NameIdentifier, usuario.Email.ToString())

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave-super-secreta-123456-pass@word1"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: "AMoralesIssuer",

                audience: "AMoralesAudience",

                claims: claims,

                expires: DateTime.Now.AddMinutes(30),

                signingCredentials: creds

            );

            return Ok(new

            {

                token = new JwtSecurityTokenHandler().WriteToken(token)

                //usuario = new { usuario.IdUsuario, usuario.Nombre, usuario.Email }

            });

        }


    }
}
