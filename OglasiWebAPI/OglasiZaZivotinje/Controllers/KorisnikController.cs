using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije s entitetom Korisnik u bazi
    /// </summary>
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly OglasiContext _context;

        public KorisnikController(OglasiContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Dohvaća sve korisnike iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Korisnik
        ///
        /// </remarks>
        /// <returns>Korisnici u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var korisnici = _context.Korisnik.ToList();
                if (korisnici == null || korisnici.Count == 0)
                {
                    return new EmptyResult();
                }

                List<KorisnikDTO> prikazi = new();
                korisnici.ForEach(k =>
                {
                    prikazi.Add(new KorisnikDTO()
                    {
                        Sifra = k.Sifra,
                        Uloga = k.Uloga,
                        Ime = k.Ime,
                        Prezime = k.Prezime,
                        Email=k.Email,
                        Mobitel=k.Mobitel,
                        Grad=k.Grad
                    });
                });

                return Ok(prikazi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }



        /// <summary>
        /// Dodaje korisnika u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Korisnik
        ///
        /// </remarks>
        /// <returns>Kreiranog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult Post(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (korisnik == null)
            {
                return BadRequest();
            }

            try
            {
                //ovdje ipak ne prikazujem KorisnikDTO, jer neki korisnici moraju upisati lozinku
                _context.Korisnik.Add(korisnik);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Mijenja korisnika sa zadanom šifrom u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}
        ///    
        /// Parametar: šifra korisnika kojeg želite mijenjati
        ///
        /// </remarks>
        /// <returns>Promijenjenog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (korisnik == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnikBaza = _context.Korisnik.Find(sifra);
                if (korisnikBaza == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                
                korisnikBaza.Uloga = korisnik.Uloga;
                korisnikBaza.Ime = korisnik.Ime;
                korisnikBaza.Prezime = korisnik.Prezime;
                korisnikBaza.Email = korisnik.Email;
                korisnikBaza.Lozinka = korisnik.Lozinka;
                korisnikBaza.Mobitel = korisnik.Mobitel;
                korisnikBaza.Grad = korisnik.Grad;

                _context.Korisnik.Update(korisnikBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, korisnikBaza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message); 
            }

        }


        /// <summary>
        /// Briše korisnika sa zadanom šifrom iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Korisnik/{sifra}
        ///    
        /// Parametar: šifra korisnika kojeg želite obrisati
        ///
        /// </remarks>
        /// <returns>Poruku da je obrisao korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{sifra:int}")]

        public IActionResult Delete(int sifra)
        {
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra poruke ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                if (korisnik.Uloga == 1 || korisnik.Uloga == 2)
                {
                    return new JsonResult("{\"poruka\":\"Ne mogu obrisati administratora ili moderatora.\"}");
                }

                _context.Korisnik.Remove(korisnik);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Korisnik obrisan.\"}");

            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Korisnik se ne može obrisati.\"}");       
            }
        }


        



    }
}
