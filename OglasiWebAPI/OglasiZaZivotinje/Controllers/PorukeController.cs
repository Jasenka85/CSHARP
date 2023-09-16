using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PorukeController: ControllerBase
    {
        private readonly OglasiContext _context;

        public PorukeController(OglasiContext context)
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
                var poruke = _context.Poruka
                    .Include(o => o.Oglas)
                    .ToList();
                
                if (poruke == null || poruke.Count == 0)
                {
                    return new EmptyResult();
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


                return Ok(prikazi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }


        [HttpPost]
        public IActionResult Post(PorukaDTO pDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (pDto.Sifra_oglasa <= 0)
            {
                return BadRequest();
            }
            try
            {
                var oglas = _context.Oglas.Find(pDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return BadRequest();
                }

                Poruka p = new Poruka()
                {
                    Oglas = oglas,
                    Ime_posiljatelja = pDto.Ime_posiljatelja,
                    Email_posiljatelja = pDto.Email_posiljatelja,
                    Tekst_poruke = pDto.Tekst_poruke,
                    Datum_poruke = pDto.Datum_poruke
                };
                _context.Poruka.Add(p);
                _context.SaveChanges();

                pDto.Sifra = p.Sifra;
                pDto.Oglas = oglas.Naslov;
                return Ok(pDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, PorukaDTO pDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || pDto == null)
            {
                return BadRequest();
            }

            try
            {
                var oglas = _context.Oglas.Find(pDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return BadRequest();
                }

                var poruka = _context.Poruka.Find(sifra);

                if (poruka == null)
                {
                    return BadRequest();
                }

                poruka.Oglas = oglas;
                poruka.Ime_posiljatelja = pDto.Ime_posiljatelja;
                poruka.Email_posiljatelja = pDto.Email_posiljatelja;
                poruka.Tekst_poruke = pDto.Tekst_poruke;
                poruka.Datum_poruke = pDto.Datum_poruke;

                _context.Poruka.Update(poruka);
                _context.SaveChanges();

                pDto.Sifra = sifra;
                pDto.Oglas = oglas.Naslov;
                return Ok(pDto);


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
                var poruka = _context.Poruka.Find(sifra);
                if (poruka == null)
                {
                    return BadRequest();
                }


                _context.Poruka.Remove(poruka);
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
