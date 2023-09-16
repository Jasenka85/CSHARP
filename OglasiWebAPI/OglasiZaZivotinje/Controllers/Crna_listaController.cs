using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Crna_listaController: ControllerBase
    {
        private readonly OglasiContext _context;

        public Crna_listaController(OglasiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
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
                    return new EmptyResult();
                }

                List<Crna_listaDTO> prikazi = new();
                clista.ForEach(c =>
                {
                    prikazi.Add(new Crna_listaDTO()
                    {
                        Sifra = c.Sifra,
                        Korisnik = c.Korisnik?.Ime + " " + c.Korisnik?.Prezime,
                        Sifra_korisnika = c.Korisnik.Sifra,
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


        [HttpPost]
        public IActionResult Post(Crna_listaDTO cDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (cDto.Sifra_korisnika <= 0)
            {
                return BadRequest();
            }
            try
            {
                var korisnik = _context.Korisnik.Find(cDto.Sifra_korisnika);

                if (korisnik == null)
                {
                    return BadRequest();
                }

                Crna_lista c = new Crna_lista()
                {
                    Korisnik = korisnik,
                    Razlog_blokiranja = cDto.Razlog_blokiranja,
                    Datum_blokiranja=cDto.Datum_blokiranja 
                };
                _context.Crna_lista.Add(c);
                _context.SaveChanges();

                cDto.Sifra = c.Sifra;
                cDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(cDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, Crna_listaDTO cDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || cDto == null)
            {
                return BadRequest();
            }

            try
            {
                var korisnik = _context.Korisnik.Find(cDto.Sifra_korisnika);

                if (korisnik == null)
                {
                    return BadRequest();
                }

                var clista = _context.Crna_lista.Find(sifra);

                if (clista == null)
                {
                    return BadRequest();
                }

                
                clista.Korisnik = korisnik;
                clista.Razlog_blokiranja = cDto.Razlog_blokiranja;
                clista.Datum_blokiranja = cDto.Datum_blokiranja;

                _context.Crna_lista.Update(clista);
                _context.SaveChanges();

                cDto.Sifra = sifra;
                cDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(cDto);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
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
                var clista = _context.Crna_lista.Find(sifra);
                if (clista == null)
                {
                    return BadRequest();
                }


                _context.Crna_lista.Remove(clista);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {
                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");

            }

        }








    }
}
