using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdministratorController: ControllerBase
    {

            private readonly OglasiContext _context;

            public AdministratorController(OglasiContext context)
            {
                _context = context;
            }

            [HttpPost("token")]
            public IActionResult GenerirajToken(Administrator admin)
            {


                var adminBaza = _context.Administrator
                       .Where(p => p.Email!.Equals(admin.Email))
                       .FirstOrDefault();

                if (adminBaza == null)
                {
                // Šaljem Status403Forbidden jer frontend hvata sve 401 i baca na login 
                // pa nikada ne dobijem poruku da nije dobro korisničko ime ili lozinka
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, ne mogu naći administratora");
                }



                if (!BCrypt.Net.BCrypt.Verify(admin.Lozinka, adminBaza.Lozinka))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, lozinka ne odgovara");
                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("MojKljucKojijeJakoMisteriozaniDugacak");


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);


                return Ok(jwt);

            }
    }
}
