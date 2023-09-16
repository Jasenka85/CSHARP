using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FotografijeController: ControllerBase
    {
        private readonly OglasiContext _context;

        public FotografijeController(OglasiContext context)
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
                var fotografije = _context.Fotografija
                    .Include(o => o.Oglas)
                    .ToList();

                if (fotografije == null || fotografije.Count == 0)
                {
                    return new EmptyResult();
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


        [HttpPost]
        public IActionResult Post(FotografijaDTO fDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (fDto.Sifra_oglasa <= 0)
            {
                return BadRequest();
            }
            try
            {
                var oglas = _context.Oglas.Find(fDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return BadRequest();
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




        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, FotografijaDTO fDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || fDto == null)
            {
                return BadRequest();
            }

            try
            {
                var oglas = _context.Oglas.Find(fDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return BadRequest();
                }

                var fotografija = _context.Fotografija.Find(sifra);

                if (fotografija == null)
                {
                    return BadRequest();
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
                var fotografija = _context.Fotografija.Find(sifra);
                if (fotografija == null)
                {
                    return BadRequest();
                }


                _context.Fotografija.Remove(fotografija);
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
