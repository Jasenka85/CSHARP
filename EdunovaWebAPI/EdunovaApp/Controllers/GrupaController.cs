using EdunovaApp.Data;
using EdunovaApp.Models;
using EdunovaApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdunovaApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije nad grupom
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GrupaController : ControllerBase
    {
        private readonly EdunovaContext _context;
        private readonly ILogger<GrupaController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        public GrupaController(EdunovaContext context, ILogger<GrupaController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam grupe");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                
                var grupe = _context.Grupa
                    .Include(g => g.Smjer)
                    .Include(g => g.Polaznici)
                    .ToList();



                if (grupe == null || grupe.Count == 0)
                {
                    return new EmptyResult();
                }

                List<GrupaDTO> vrati = new();
                grupe.ForEach(g =>
                {
                    vrati.Add(new GrupaDTO()
                    {
                        Sifra = g.Sifra,
                        Naziv = g.Naziv,
                        Smjer = g.Smjer?.Naziv,
                        SifraSmjer = g.Smjer.Sifra,
                        DatumPocetka = g.DatumPocetka,
                        BrojPolaznika = g.Polaznici.Count
                    });
                });

                return Ok(vrati);
                


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }


        [HttpPost]

        public IActionResult Post(GrupaDTO grupaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (grupaDTO.SifraSmjer <= 0)
            {
                return BadRequest(ModelState);
            }


            try
            {



                var smjer = _context.Smjer.Find(grupaDTO.SifraSmjer);

                if (smjer == null)
                {
                    return BadRequest(ModelState);
                }

                Grupa g = new()
                {
                    Naziv = grupaDTO.Naziv,
                    Smjer = smjer,
                    DatumPocetka = grupaDTO.DatumPocetka

                };

                _context.Grupa.Add(g);
                _context.SaveChanges();

                grupaDTO.Sifra = g.Sifra;
                grupaDTO.Smjer = smjer.Naziv;

                return Ok(grupaDTO);

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);

            }


        }



        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, GrupaDTO grupaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || grupaDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var smjer = _context.Smjer.Find(grupaDTO.SifraSmjer);

                if (smjer == null)
                {
                    return BadRequest();

                }

                var grupa = _context.Grupa.Find(sifra);

                if (grupa == null)
                {
                    return BadRequest();

                }

                grupa.Naziv = grupaDTO.Naziv;
                grupa.Smjer = smjer;
                grupa.DatumPocetka = grupaDTO.DatumPocetka;

                _context.Grupa.Update(grupa);
                _context.SaveChanges();

                grupaDTO.Sifra = sifra;
                grupaDTO.Smjer = smjer.Naziv;

                return Ok(grupaDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }



        [HttpDelete]
        [Route("{sifra:int}")]

        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var grupaBaza = _context.Grupa.Find(sifra);
                if (grupaBaza == null)
                {
                    return BadRequest();
                }

                _context.Grupa.Remove(grupaBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception ex)
            {
                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");
            }
        }


        [HttpGet]
        [Route("{sifra:int}/polaznici")]

        public IActionResult GetPolaznici(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var grupa = _context.Grupa
                    .Include(g => g.Polaznici)
                    .FirstOrDefault(g => g.Sifra == sifra);

                if (grupa == null)
                {
                    return BadRequest();
                }

                if (grupa.Polaznici == null || grupa.Polaznici.Count == 0)
                {
                    return new EmptyResult();
                }

                List<PolaznikDTO> vrati = new();
                grupa.Polaznici.ForEach(p =>
                {
                    vrati.Add(new PolaznikDTO()
                    {
                        Sifra = p.Sifra,
                        Ime = p.Ime,
                        Prezime = p.Prezime,
                        Oib = p.Oib,
                        Email = p.Email
                    });
                });

                return Ok(vrati);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        [HttpPost]
        [Route("{sifra:int}/dodaj/{polaznikSifra:int}")]
        public IActionResult DodajPolaznika(int sifra, int polaznikSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || polaznikSifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var grupa = _context.Grupa
                    .Include(g => g.Polaznici)
                    .FirstOrDefault(g => g.Sifra == sifra);

                if (grupa == null)
                {
                    return BadRequest();
                }

                var polaznik = _context.Polaznik.Find(polaznikSifra);

                if (polaznik == null)
                {
                    return BadRequest();
                }

                // napraviti kontrolu da li je taj polaznik već u toj grupi
                grupa.Polaznici.Add(polaznik);

                _context.Grupa.Update(grupa);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status503ServiceUnavailable,
                       ex.Message);
            }

        }



        [HttpDelete]
        [Route("{sifra:int}/obrisi/{polaznikSifra:int}")]
        public IActionResult ObrisiPolaznika(int sifra, int polaznikSifra)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || polaznikSifra <= 0)
            {
                return BadRequest();
            }

            try
            {

                var grupa = _context.Grupa
                    .Include(g => g.Polaznici)
                    .FirstOrDefault(g => g.Sifra == sifra);

                if (grupa == null)
                {
                    return BadRequest();
                }

                var polaznik = _context.Polaznik.Find(polaznikSifra);

                if (polaznik == null)
                {
                    return BadRequest();
                }


                grupa.Polaznici.Remove(polaznik);

                _context.Grupa.Update(grupa);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status503ServiceUnavailable,
                       ex.Message);

            }

        }




    }
}
