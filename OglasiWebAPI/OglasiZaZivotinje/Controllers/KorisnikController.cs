using Microsoft.AspNetCore.Mvc;
using OglasiZaZivotinje.Models;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KorisnikController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var lista = new List<Korisnik>()
            {
            new()
            {
                Sifra = 1,
                Uloga = 1,      // administrator
                Lozinka = "Zoki123",
                Ime = "Sanja",
                Prezime = "Habuš",
                Email = "shabus@gmail.com",
                Mobitel = "092 146 3753",
                Grad = "Zaprešić"
            },

            new()
            {
                Sifra = 2,
                Uloga = 1,      // administrator
                Lozinka = "Bruno123",
                Ime = "Jasenka",
                Prezime = "Augustinović",
                Email = "jaugustinovic@gmail.com",
                Mobitel = "091 543 6424",
                Grad = "Osijek"
            },

            new()
            {
                Sifra = 3,
                Uloga = 2,      // moderator
                Lozinka = "Ivan123",
                Ime = "Ana",
                Prezime = "Marasović",
                Email = "amarasovic@gmail.com",
                Mobitel = "099 234 4422",
                Grad = "Zagreb"
            },

            new()
            {
                Sifra = 4,
                Uloga = 2,      // moderator
                Lozinka = "Josip123",
                Ime = "Maja",
                Prezime = "Grgić",
                Email = "mgrgic@gmail.com",
                Mobitel = "095 632 7455",
                Grad = "Sesvete"

            },

            new() {
                Sifra = 5,
                Uloga = 0,      // obican korisnik
                Lozinka = "",
                Ime = "Ivana",
                Prezime = "Banić",
                Email = "ivana.banic@gmail.com",
                Mobitel = "091 555 7654",
                Grad = "Našice"
            },

            new()
            {
                Sifra = 6,
                Uloga = 0,      // obican korisnik
                Lozinka = "",
                Ime = "Adriana",
                Prezime = "Popović",
                Email = "apopovic@gmail.com",
                Mobitel = "098 323 7532",
                Grad = "Tenja"
            }
            };

            return new JsonResult(lista);
        
        }

        [HttpPost]

        public IActionResult Post(Korisnik korisnik)
        {
            return Created("api/v1/Korisnik", korisnik);
        }

        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, Korisnik korisnik)
        {
            return StatusCode(StatusCodes.Status200OK, korisnik);
        }

        [HttpDelete]
        [Route("{sifra:int}")]

        public IActionResult Delete(int sifra)
        {
            return StatusCode(StatusCodes.Status200OK, "{\"obrisano\":true}");
        }



    }
}
