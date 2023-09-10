using EdunovaApp.Data;
using EdunovaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EdunovaApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetom smjer u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

        
    public class SmjerController: ControllerBase
    {
        


        //dependency injection u controller -> vidi link!

        private readonly EdunovaContext _context;

        
        public SmjerController(EdunovaContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Dohvaća sve smjerove iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Smjer
        ///
        /// </remarks>
        /// <returns>Smjerovi u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]
        public IActionResult Get()
        {
            // kontrola ukoliko upit nije dobar
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var smjerovi = _context.Smjer.ToList();
                if (smjerovi == null || smjerovi.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Smjer.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                    ex.Message);
            }
            
        }


        /// <summary>
        /// Dodaje smjer u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Smjer
        ///    {naziv:"",trajanje:100}
        ///
        /// </remarks>
        /// <returns>Kreirani smjer u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]

        public IActionResult Post(Smjer smjer) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Smjer.Add(smjer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, smjer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                   ex.Message);
            }
            
        }


        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, Smjer smjer)
        {
            if (sifra <= 0 || smjer == null)
            {
                return BadRequest();
            }

            try
            {
                var smjerBaza = _context.Smjer.Find(sifra);
                if (smjerBaza == null)
                {
                    return BadRequest();
                }
                // inače se rade Mapper-i
                // mi ćemo za sada ručno
                smjerBaza.Naziv = smjer.Naziv;
                smjerBaza.Trajanje = smjer.Trajanje;
                smjerBaza.Cijena = smjer.Cijena;
                smjerBaza.Upisnina = smjer.Upisnina;
                smjerBaza.Verificiran = smjer.Verificiran;

                _context.Smjer.Update(smjerBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, smjerBaza);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  ex); // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
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
                var smjerBaza = _context.Smjer.Find(sifra);
                if (smjerBaza == null)
                {
                    return BadRequest();
                }

                _context.Smjer.Remove(smjerBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {

                try
                {
                    SqlException sqle = (SqlException)ex;
                    return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  sqle);
                }
                catch (Exception e)
                {

                }

                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  ex);
            }
        }
    }
}
