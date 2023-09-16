using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OglasiController: ControllerBase
    {
            private readonly OglasiContext _context;

            public OglasiController(OglasiContext context)
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
                var oglasi = _context.Oglas
                    .Include(k => k.Korisnik)
                    .ToList();
                if (oglasi == null || oglasi.Count == 0)
                {
                    return new EmptyResult();
                }

                List<OglasDTO> prikazi = new();
                oglasi.ForEach(o =>
                {
                    prikazi.Add(new OglasDTO()
                    {
                        Sifra = o.Sifra,
                        Aktivan = o.Aktivan,
                        Korisnik = o.Korisnik?.Ime + " " + o.Korisnik?.Prezime,
                        Sifra_korisnika = o.Korisnik.Sifra,
                        Kategorija = o.Kategorija,
                        Datum_objave = o.Datum_objave,
                        Naslov = o.Naslov,
                        Opis = o.Opis,
                        Vrsta_zivotinje=o.Vrsta_zivotinje,
                        Ime_zivotinje=o.Ime_zivotinje,
                        Spol_zivotinje=o.Spol_zivotinje,
                        Dob_zivotinje=o.Dob_zivotinje,
                        Kastriran=o.Kastriran
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

        public IActionResult Post(OglasDTO oDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (oDto.Sifra_korisnika <= 0)
            {
                return BadRequest();
            }
            try
            {
                var korisnik = _context.Korisnik.Find(oDto.Sifra_korisnika);

                if (korisnik == null)
                { 
                return BadRequest();
                }

                Oglas o = new Oglas()
                {
                    Aktivan = oDto.Aktivan,
                    Korisnik = korisnik,
                    Kategorija = oDto.Kategorija,
                    Datum_objave = oDto.Datum_objave,
                    Naslov = oDto.Naslov,
                    Opis = oDto.Opis,
                    Vrsta_zivotinje = oDto.Vrsta_zivotinje,
                    Ime_zivotinje = oDto.Ime_zivotinje,
                    Spol_zivotinje = oDto.Spol_zivotinje,
                    Dob_zivotinje = oDto.Dob_zivotinje,
                    Kastriran = oDto.Kastriran
                };
                _context.Oglas.Add(o);
                _context.SaveChanges();

                oDto.Sifra = o.Sifra;
                oDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(oDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, OglasDTO oDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || oDto == null)
            {
                return BadRequest();
            }

            try
            {
                var korisnik = _context.Korisnik.Find(oDto.Sifra_korisnika);
                
                if (korisnik == null)
                {
                    return BadRequest();
                }

                var oglas = _context.Oglas.Find(sifra);

                if (oglas == null)
                {
                    return BadRequest();
                }

                oglas.Aktivan = oDto.Aktivan;
                oglas.Korisnik = korisnik;
                oglas.Kategorija = oDto.Kategorija;
                oglas.Datum_objave = oDto.Datum_objave;
                oglas.Naslov = oDto.Naslov;
                oglas.Opis = oDto.Opis;
                oglas.Vrsta_zivotinje = oDto.Vrsta_zivotinje;
                oglas.Ime_zivotinje = oDto.Ime_zivotinje;
                oglas.Spol_zivotinje = oDto.Spol_zivotinje;
                oglas.Dob_zivotinje = oDto.Dob_zivotinje;
                oglas.Kastriran = oDto.Kastriran;

                _context.Oglas.Update(oglas);
                _context.SaveChanges();

                oDto.Sifra = sifra;
                oDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(oDto);

               
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
                var oglas = _context.Oglas.Find(sifra);
                if (oglas == null)
                {
                    return BadRequest();
                }
                

                _context.Oglas.Remove(oglas);
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
