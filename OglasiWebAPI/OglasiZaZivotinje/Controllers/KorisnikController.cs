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
        /// <returns>Svi korisnici u bazi</returns>
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
                        //lozinka se neće prikazivati
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
        /// Dohvaća korisnika sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Korisnik/{sifra}
        ///
        /// </remarks>
        /// <returns>Korisnika sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam korisnika sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                return new JsonResult(korisnik);
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

                return Ok(kDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Mijenja podatke korisnika sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}
        ///    
        /// Parametar: šifra korisnika kojeg želite mijenjati
        ///
        /// Napomena: "sifra" se dohvaća iz baze
        /// Uloga i lozinka se ne mogu mijenjati, to može samo administrator u posebnoj ruti
        /// 
        /// </remarks>
        /// <returns>Promijenjenog korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KorisnikDTO kDto)
        {

            _logger.LogInformation("Mijenjam podatke korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (kDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                
                //uloga se ne mijenja
                korisnik.Ime = kDto.Ime;
                korisnik.Prezime = kDto.Prezime;
                korisnik.Email = kDto.Email;
                //lozinka se ne mijenja
                korisnik.Mobitel = kDto.Mobitel;
                korisnik.Grad = kDto.Grad;

                _context.Korisnik.Update(korisnik);
                _context.SaveChanges();

                kDto.Sifra = korisnik.Sifra;   //dohvatim sifru iz baze
                kDto.Uloga = korisnik.Uloga;   //pregazim ono što je unio korisnik
                //lozinka se ne prikazuje

                return Ok(kDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message); 
            }

        }




        /// <summary>
        /// Mijenja ulogu i lozinku korisnika sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}/Uloga
        ///    
        /// Parametri: šifra korisnika, nova uloga i lozinka
        ///
        /// Uloge: 0 = korisnik, 1 = administrator, 2 = moderator
        /// Lozinka je potrebna samo za administratora i moderatora.
        /// 
        /// </remarks>
        /// <returns>Promijenjenog korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}/Uloga")]
        public IActionResult PromjenaUloge(int sifra, int uloga, string? lozinka)
        {

            _logger.LogInformation("Mijenjam ulogu korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }
            if (uloga < 0 || uloga > 2)
            {
                return new JsonResult("{\"poruka\":\"Uloga korisnika može biti samo 0, 1 ili 2.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                if (korisnik.Uloga == 3)
                {
                    return new JsonResult("{\"poruka\":\"Korisnik je na crnoj listi!\"}");
                }

                korisnik.Uloga = uloga;
                korisnik.Lozinka = lozinka;

                _context.Korisnik.Update(korisnik);
                _context.SaveChanges();

                return Ok(korisnik);
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
