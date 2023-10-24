using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{

    /// <summary>
    /// Namijenjeno za CRUD operacije s entitetom Poruka u bazi
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PorukeController: ControllerBase
    {
        private readonly OglasiContext _context;
        private readonly ILogger<PorukeController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public PorukeController(OglasiContext context, ILogger<PorukeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Dohvaća sve poruke u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Poruke
        ///
        /// </remarks>
        /// <returns>Sve poruke u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam poruke...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var poruke = _context.Poruka
                    .Include(o => o.Oglas)
                    .ToList();
                
                if (poruke == null || poruke.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<PorukaDTO> prikazi = new();
                poruke.ForEach(p =>
                {
                    prikazi.Add(new PorukaDTO()
                    {
                        Sifra = p.Sifra,         
                        Oglas = p.Oglas?.Naslov,
                        Sifra_oglasa = p.Oglas.Sifra,
                        Ime_posiljatelja = p.Ime_posiljatelja,
                        Email_posiljatelja = p.Email_posiljatelja,
                        Tekst_poruke = p.Tekst_poruke,
                        Datum_poruke = p.Datum_poruke
                    });
                });

                return new JsonResult(prikazi.OrderByDescending(p => p.Sifra));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }


        /// <summary>
        /// Dohvaća poruku sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Poruke/{sifra}
        ///
        /// </remarks>
        /// <returns>Poruku sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam poruku sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra poruke ne može biti manja od 1.\"}");
            }

            try
            {
                var poruka = _context.Poruka
                   .Include(c => c.Oglas)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (poruka == null || poruka.Oglas == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var trazen = new PorukaDTO()
                {
                    Sifra = poruka.Sifra,
                    Oglas = poruka.Oglas?.Naslov,
                    Sifra_oglasa = poruka.Oglas.Sifra,
                    Ime_posiljatelja = poruka.Ime_posiljatelja,
                    Email_posiljatelja = poruka.Email_posiljatelja,
                    Tekst_poruke = poruka.Tekst_poruke,
                    Datum_poruke = poruka.Datum_poruke
                };

                return new JsonResult(trazen);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        /// <summary>
        /// Dohvaća sve poruke za traženi oglas
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Poruke/{sifra}/Oglas
        ///
        /// Parametar: šifra oglasa za kojeg želite pregledati poruke
        /// 
        /// </remarks>
        /// <returns>Sve poruke za traženi oglas</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("Oglas/{sifra:int}")]
        public IActionResult PorukeOglasa(int sifra)
        {
            _logger.LogInformation("Dohvaćam poruke za dani oglas...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var poruke = _context.Poruka
                    .Include(o => o.Oglas)
                    .ToList();

                if (poruke == null || poruke.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var porukeoglasa = new List<Poruka>();

                foreach (Poruka p in poruke)
                {
                    if (p.Oglas.Sifra == sifra)
                    {
                        porukeoglasa.Add(p);
                    }
                }

                if (porukeoglasa.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var prikazi = new List<PorukaDTO>();

                porukeoglasa.ForEach(p =>
                {
                    prikazi.Add(new PorukaDTO()
                    {
                        Sifra = p.Sifra,
                        Oglas = p.Oglas?.Naslov,
                        Sifra_oglasa = p.Oglas.Sifra,
                        Ime_posiljatelja = p.Ime_posiljatelja,
                        Email_posiljatelja = p.Email_posiljatelja,
                        Tekst_poruke = p.Tekst_poruke,
                        Datum_poruke = p.Datum_poruke
                    });
                });
                
                return new JsonResult(prikazi.OrderByDescending(p => p.Sifra));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        /// <summary>
        /// Dodaje novu poruku u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Poruke
        ///    
        /// Napomena: "sifra" i "oglas" se dohvaćaju iz baze, 
        /// "datum_poruke" se bilježi automatski
        /// 
        /// Potrebno je unijeti šifru oglasa za kojeg šaljete poruku (sifra_oglasa)
        /// 
        /// </remarks>
        /// <returns>Kreiranu poruku u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult Post(PorukaDTO pDto)
        {
            _logger.LogInformation("Dodajem novu poruku...");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (pDto == null)
            {
                return BadRequest();
            }
            if (pDto.Sifra_oglasa < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }
            try
            {
                var oglas = _context.Oglas.Find(pDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                Poruka p = new Poruka()
                {
                    Oglas = oglas,
                    Ime_posiljatelja = pDto.Ime_posiljatelja,
                    Email_posiljatelja = pDto.Email_posiljatelja,
                    Tekst_poruke = pDto.Tekst_poruke,
                    Datum_poruke = DateTime.Now
                };
                _context.Poruka.Add(p);
                _context.SaveChanges();

                pDto.Sifra = p.Sifra;
                pDto.Oglas = oglas.Naslov;
                pDto.Datum_poruke = p.Datum_poruke;  //pregazim datum koji je unio korisnik
                return Ok(pDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }


        /// <summary>
        /// Mijenja poruku sa zadanom šifrom u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Poruke/{sifra}
        ///
        /// Parametar: šifra poruke koju želite mijenjati
        /// 
        /// 
        /// Napomena: "oglas" i "sifra_oglasa" se dohvaćaju iz baze, 
        /// "datum_poruke" se bilježi automatski i ne može se mijenjati.
        /// 
        /// </remarks>
        /// <returns>Promijenjenu poruku u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, PorukaDTO pDto)
        {
            _logger.LogInformation("Mijenjam poruku...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (pDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var poruka = _context.Poruka
                   .Include(c => c.Oglas)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (poruka == null || poruka.Oglas == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                

                poruka.Ime_posiljatelja = pDto.Ime_posiljatelja;
                poruka.Email_posiljatelja = pDto.Email_posiljatelja;
                poruka.Tekst_poruke = pDto.Tekst_poruke;
                //datum poruke ne može se mijenjati

                _context.Poruka.Update(poruka);
                _context.SaveChanges();

                pDto.Sifra = sifra;
                pDto.Oglas = poruka.Oglas.Naslov;
                pDto.Datum_poruke = poruka.Datum_poruke;   //pregazim ono što je unio korisnik
                pDto.Sifra_oglasa = poruka.Oglas.Sifra;
                return Ok(pDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
            }

        }




        /// <summary>
        /// Briše poruku sa zadanom šifrom iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Poruke/{sifra}
        ///
        /// Parametar: šifra poruke koju želite obrisati
        /// 
        /// </remarks>
        /// <returns>Obavijest da je obrisao poruku</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            _logger.LogInformation("Brišem poruku...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra poruke ne može biti manja od 1.\"}");
            }

            try
            {
                var poruka = _context.Poruka.Find(sifra);
                if (poruka == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                _context.Poruka.Remove(poruka);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Poruka obrisana.\"}");
            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Poruka nije obrisana.\"}");
            }

        }






    }
}
