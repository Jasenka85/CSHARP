using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije s entitetom Crna_lista u bazi
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class Crna_listaController: ControllerBase
    {
        private readonly OglasiContext _context;
        private readonly ILogger<Crna_listaController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public Crna_listaController(OglasiContext context, ILogger<Crna_listaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Dohvaća sve korisnike iz baze koji su na crnoj listi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Crna_lista
        ///
        /// </remarks>
        /// <returns>Korisnike iz baze koji su na crnoj listi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam korisnike na crnoj listi...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var clista = _context.Crna_lista
                    .Include(k => k.Korisnik)
                    .ToList();
                if (clista == null || clista.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<Crna_listaDTO> prikazi = new();
                clista.ForEach(c =>
                {
                    prikazi.Add(new Crna_listaDTO()
                    {
                        Sifra = c.Sifra,
                        Korisnik = c.Korisnik?.Ime + " " + c.Korisnik?.Prezime,
                        Sifra_korisnika = c.Korisnik.Sifra,
                        Email_korisnika = c.Korisnik.Email,
                        Mobitel_korisnika = c.Korisnik.Mobitel,
                        Grad_korisnika = c.Korisnik.Grad,
                        Razlog_blokiranja = c.Razlog_blokiranja,
                        Datum_blokiranja=c.Datum_blokiranja
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
        /// Dohvaća zapis u crnoj listi sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Crna_lista/{sifra}
        ///
        /// </remarks>
        /// <returns>Zapis u crnoj listi sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam zapis sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var trazenC = _context.Crna_lista
                    .Include(c => c.Korisnik)
                    .FirstOrDefault(x => x.Sifra == sifra);


                if (trazenC == null || trazenC.Korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var trazenDto = new Crna_listaDTO()
                {
                    Sifra = trazenC.Sifra,
                    Korisnik = trazenC.Korisnik?.Ime + " " + trazenC.Korisnik?.Prezime,
                    Sifra_korisnika = trazenC.Korisnik.Sifra,
                    Email_korisnika = trazenC.Korisnik.Email,
                    Mobitel_korisnika = trazenC.Korisnik.Mobitel,
                    Grad_korisnika = trazenC.Korisnik.Grad,
                    Razlog_blokiranja = trazenC.Razlog_blokiranja,
                    Datum_blokiranja = trazenC.Datum_blokiranja
                };
                return new JsonResult(trazenDto);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }





        /// <summary>
        /// Dodaje novog korisnika na crnu listu u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Crna_lista
        ///    
        /// Napomena: "sifra" i "korisnik" se dohvaćaju iz baze, 
        /// "datum_blokiranja" se bilježi automatski
        /// 
        /// Potrebno je unijeti šifru korisnika kojeg stavljate na crnu listu (sifra_korisnika)
        /// 
        /// Korisnik na crnoj listi je blokiran i ne može objaviti novi oglas.
        /// Preporučuje se deaktivirati sve njegove prijašnje oglase.
        /// 
        /// </remarks>
        /// <returns>Novi unos u crnu listu u bazi, sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult Post(Crna_listaDTO cDto)
        {
            _logger.LogInformation("Dodajem korisnika na crnu listu...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (cDto.Sifra_korisnika < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            //prvo provjeravam postoji li već korisnik s tom šifrom na crnoj listi u bazi
            var korisnikcl = _context.Crna_lista
                .Include(k => k.Korisnik)
                .FirstOrDefault(k => k.Korisnik.Sifra == cDto.Sifra_korisnika);
            
            //ako postoji, ispiši poruku i izađi
            if (korisnikcl != null)
            {
                return new JsonResult("{\"poruka\":\"Korisnik je već na crnoj listi!\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(cDto.Sifra_korisnika);

                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                if (korisnik.Uloga == 1 || korisnik.Uloga == 2)
                {
                    return new JsonResult("{\"poruka\":\"Ne mogu blokirati administratora i moderatora.\"}");
                }

                korisnik.Uloga = 3; //postavi korisniku ulogu 3 = blokiran
                _context.Korisnik.Update(korisnik);
                _context.SaveChanges();

                Crna_lista c = new Crna_lista()
                {
                    Korisnik = korisnik,
                    Razlog_blokiranja = cDto.Razlog_blokiranja,
                    Datum_blokiranja= DateTime.Now 
                };
                _context.Crna_lista.Add(c);
                _context.SaveChanges();

                cDto.Sifra = c.Sifra;
                cDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                cDto.Email_korisnika = korisnik.Email;
                cDto.Mobitel_korisnika = korisnik.Mobitel;
                cDto.Grad_korisnika = korisnik.Grad;
                cDto.Datum_blokiranja = c.Datum_blokiranja;  //pregazim datum koji je unio korisnik
                
                return Ok(cDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        /// <summary>
        /// Mijenja unos u crnoj listi sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Crna_lista/{sifra}
        ///
        /// Parametar: šifra unosa u crnu listu kojeg želite mijenjati
        /// 
        /// 
        /// Napomena: "korisnik" i "sifra_korisnika" se dohvaćaju iz baze, 
        /// "datum_blokiranja" se bilježi automatski i ne može se mijenjati.
        /// 
        /// </remarks>
        /// <returns>Promijenjen unos u crnu listu sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Crna_listaDTO cDto)
        {
            _logger.LogInformation("Mijenjam unos u crnoj listi...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (cDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var trazenC = _context.Crna_lista
                   .Include(c => c.Korisnik)
                   .FirstOrDefault(x => x.Sifra == sifra);


                if (trazenC == null || trazenC.Korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                trazenC.Razlog_blokiranja = cDto.Razlog_blokiranja;
                //korisnik i datum blokiranja ne mogu se mijenjati

                _context.Crna_lista.Update(trazenC);
                _context.SaveChanges();

                cDto.Sifra = sifra;
                cDto.Korisnik = trazenC.Korisnik?.Ime + " " + trazenC.Korisnik?.Prezime;
                cDto.Sifra_korisnika = trazenC.Korisnik.Sifra;
                cDto.Email_korisnika = trazenC.Korisnik.Email;
                cDto.Mobitel_korisnika = trazenC.Korisnik.Mobitel;
                cDto.Grad_korisnika = trazenC.Korisnik.Grad;
                cDto.Datum_blokiranja = trazenC.Datum_blokiranja;    //pregazim ono što je upisao korisnik
                return Ok(cDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
            }

        }




        /// <summary>
        /// Briše unos iz crne liste sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Crna_lista/{sifra}
        ///
        /// Parametar: šifra unosa u crnoj listi kojeg želite obrisati
        /// 
        /// </remarks>
        /// <returns>Obavijest da je obrisao unos u crnoj listi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            _logger.LogInformation("Brišem unos iz crne liste...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra unosa u crnoj listi ne može biti manja od 1.\"}");
            }

            try
            {
                var trazenC = _context.Crna_lista
                   .Include(c => c.Korisnik)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (trazenC == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                trazenC.Korisnik.Uloga = 0;
                _context.Korisnik.Update(trazenC.Korisnik);
                _context.SaveChanges();

                _context.Crna_lista.Remove(trazenC);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Unos iz crne liste obrisan.\"}");
            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Unos iz crne liste nije obrisan.\"}");
            }
        }








    }
}
