using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije s entitetom Fotografija u bazi
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class FotografijeController: ControllerBase
    {
        private readonly OglasiContext _context;
        private readonly ILogger<FotografijeController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public FotografijeController(OglasiContext context, ILogger<FotografijeController> logger)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Dohvaća sve fotografije u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Fotografije
        ///
        /// </remarks>
        /// <returns>Sve fotografije u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam fotografije...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fotografije = _context.Fotografija
                    .Include(o => o.Oglas)
                    .ToList();

                if (fotografije == null || fotografije.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<FotografijaDTO> prikazi = new();
                fotografije.ForEach(f =>
                {
                    prikazi.Add(new FotografijaDTO()
                    {
                        Sifra = f.Sifra,
                        Oglas = f.Oglas?.Naslov,
                        Sifra_oglasa = f.Oglas.Sifra,
                        Naziv = f.Naziv,
                        Link = f.Link
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
        /// Dohvaća sve fotografije iz traženog oglasa
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Fotografije/{sifra}/Oglas
        ///
        /// Parametar: šifra oglasa u kojem želite pregledati fotografije
        /// 
        /// </remarks>
        /// <returns>Sve fotografije iz traženog oglasa</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}/Oglas")]
        public IActionResult FotografijeOglasa(int sifra)
        {
            _logger.LogInformation("Dohvaćam fotografije iz oglasa...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var fotografije = _context.Fotografija
                    .Include(f => f.Oglas)
                    .ToList();

                if (fotografije == null || fotografije.Count == 0)
                {
                    return new JsonResult("{\"poruka\":\"Nema fotografija na listi.\"}");
                }

                var fotografijeoglasa = new List<Fotografija>();

                foreach (Fotografija f in fotografije)
                {
                    if (f.Oglas.Sifra == sifra)
                    {
                        fotografijeoglasa.Add(f);
                    }
                }

                if (fotografijeoglasa.Count == 0)
                {
                    return new JsonResult("{\"poruka\":\"U oglasu nema fotografija.\"}");
                }

                var prikazi = new List<FotografijaDTO>();

                fotografijeoglasa.ForEach(f =>
                {
                    prikazi.Add(new FotografijaDTO()
                    {
                        Sifra = f.Sifra,
                        Oglas = f.Oglas?.Naslov,
                        Sifra_oglasa = f.Oglas.Sifra,
                        Naziv = f.Naziv,
                        Link = f.Link
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
        /// Dodaje novu fotografiju u oglas
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Fotografije
        ///    
        /// Napomena: "sifra" i "oglas" se dohvaćaju iz baze
        /// 
        /// Potrebno je unijeti šifru oglasa u kojeg stavljate fotografiju (sifra_oglasa)
        /// 
        /// </remarks>
        /// <returns>Novu fotografiju u oglasu, sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>

        [HttpPost]
        public IActionResult Post(FotografijaDTO fDto)
        {
            _logger.LogInformation("Dodajem novu fotografiju...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (fDto == null)
            {
                return BadRequest();
            }
            if (fDto.Sifra_oglasa < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }
            
            try
            {
                var oglas = _context.Oglas.Find(fDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }

                Fotografija f = new Fotografija()
                {
                    Oglas = oglas,
                    Naziv = fDto.Naziv,
                    Link = fDto.Link  
                };
                _context.Fotografija.Add(f);
                _context.SaveChanges();

                fDto.Sifra = f.Sifra;
                fDto.Oglas = oglas.Naslov;
                return Ok(fDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        /// <summary>
        /// Mijenja fotografiju sa zadanom šifrom u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Fotografije/{sifra}
        ///
        /// Parametar: šifra fotografije koju želite mijenjati
        /// 
        /// Potrebno je unijeti i šifru oglasa u kojem se nalazi fotografija (sifra_oglasa).
        /// 
        /// Napomena: "oglas" se dohvaća iz baze
        /// 
        /// </remarks>
        /// <returns>Promijenjenu fotografiju u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, FotografijaDTO fDto)
        {
            _logger.LogInformation("Mijenjam fotografiju...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (fDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1 || fDto.Sifra_oglasa < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var oglas = _context.Oglas.Find(fDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }

                var fotografija = _context.Fotografija.Find(sifra);

                if (fotografija == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema fotografije s tom šifrom.\"}");
                }

                fotografija.Oglas = oglas;
                fotografija.Naziv = fDto.Naziv;
                fotografija.Link = fDto.Link;
                
                _context.Fotografija.Update(fotografija);
                _context.SaveChanges();

                fDto.Sifra = sifra;
                fDto.Oglas = oglas.Naslov;
                return Ok(fDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
            }
        }





        /// <summary>
        /// Briše fotografiju sa zadanom šifrom iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Fotografije/{sifra}
        ///
        /// Parametar: šifra fotografije koju želite obrisati
        /// 
        /// </remarks>
        /// <returns>Obavijest da je obrisao fotografiju</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            _logger.LogInformation("Brišem fotografiju...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra fotografije ne može biti manja od 1.\"}");
            }

            try
            {
                var fotografija = _context.Fotografija.Find(sifra);
                if (fotografija == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema fotografije s tom šifrom.\"}");
                }

                _context.Fotografija.Remove(fotografija);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Fotografija obrisana.\"}");

            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Fotografija se ne može obrisati.\"}");

            }

        }






    }
}
