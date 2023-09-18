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
        private readonly ILogger<KorisnikController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public KorisnikController(OglasiContext context, ILogger<KorisnikController> logger)
        {
            _context = context;
            _logger = logger;   
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
            _logger.LogInformation("Dohvaćam korisnike...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var korisnici = _context.Korisnik.ToList();

                if (korisnici == null || korisnici.Count == 0)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika na listi.\"}");
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
        /// Napomena: "sifra" se dohvaća iz baze,
        /// novi korisnik uvijek ima ulogu 0 (običan korisnik)
        /// 
        /// </remarks>
        /// <returns>Kreiranog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult Post(KorisnikDTO kDto)
        {
            _logger.LogInformation("Dodajem novog korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (kDto == null)
            {
                return BadRequest();
            }

            try
            {
                Korisnik k = new Korisnik()
                {
                    Uloga = 0,  //novi korisnik je u početku običan korisnik (uloga=0)
                                //samo administrator može promijeniti ulogu i dodati lozinku
                    Ime = kDto.Ime,
                    Prezime = kDto.Prezime,
                    Email = kDto.Email,
                    Lozinka = "",   //lozinka će se popuniti samo ako član postane administrator ili moderator
                    Mobitel = kDto.Mobitel,
                    Grad = kDto.Grad
                };
                
                _context.Korisnik.Add(k);
                _context.SaveChanges();

                kDto.Sifra = k.Sifra; //dohvatim šifru iz baze
                kDto.Uloga = 0;   //pregazim ono što je upisao korisnik

                return StatusCode(StatusCodes.Status201Created, kDto);
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
        /// Napomena: "sifra" se dohvaća iz baze
        /// Uloge: 0 = korisnik, 1 = administrator, 2 = moderator, 3 = blokiran
        /// Lozinka je potrebna samo za administratora i moderatora.
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
            //ovdje ne koristim KorisnikDTO, da admin može promijeniti ulogu
            //i dodijeliti lozinku ako je potrebno

            _logger.LogInformation("Mijenjam korisnika...");

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
        /// Napomena: nije moguće obrisati administratora i moderatora,
        /// kao ni korisnika koji ima objavljen oglas.
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
            _logger.LogInformation("Brišem korisnika...");

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
