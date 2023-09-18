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

        public PorukeController(OglasiContext context)
        {
            _context = context;
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
        [Route("{sifra:int}/Oglas")]
        public IActionResult PorukeOglasa(int sifra)
        {
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
                    return new EmptyResult();
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
                    return new JsonResult("{\"poruka\":\"Nema poruka za taj oglas.\"}");
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
                return Ok(prikazi);

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
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
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
        /// Potrebno je unijeti i šifru oglasa kojem se šalje poruka (sifra_oglasa).
        /// 
        /// Napomena: "oglas" se dohvaća iz baze, 
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (pDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1 || pDto.Sifra_oglasa < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var oglas = _context.Oglas.Find(pDto.Sifra_oglasa);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }

                var poruka = _context.Poruka.Find(sifra);

                if (poruka == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema poruke s tom šifrom.\"}");
                }

                poruka.Oglas = oglas;
                poruka.Ime_posiljatelja = pDto.Ime_posiljatelja;
                poruka.Email_posiljatelja = pDto.Email_posiljatelja;
                poruka.Tekst_poruke = pDto.Tekst_poruke;
                //datum poruke ne može se mijenjati

                _context.Poruka.Update(poruka);
                _context.SaveChanges();

                pDto.Sifra = sifra;
                pDto.Oglas = oglas.Naslov;
                pDto.Datum_poruke = poruka.Datum_poruke;   //pregazim ono što je unio korisnik
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
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra poruke ne može biti manja od 1.\"}");
            }

            try
            {
                var poruka = _context.Poruka.Find(sifra);
                if (poruka == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema poruke s tom šifrom.\"}");
                }

                _context.Poruka.Remove(poruka);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Poruka obrisana.\"}");
            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Poruka se ne može obrisati.\"}");
            }

        }






    }
}
