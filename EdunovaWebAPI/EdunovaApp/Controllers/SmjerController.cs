using EdunovaApp.Data;
using EdunovaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EdunovaApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije nad entitetom smjer u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]    
    public class SmjerController: ControllerBase
    {

        //dependency injection u controller -> vidi link!
        //https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio#dependency-injection
        
        private readonly EdunovaContext _context;
        //read only jer nećemo mijenjati taj objekt, nego ćemo ga koristiti za promjenu podataka u bazi

        
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
        ///    GET api/v1/smjer
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
        ///    POST api/v1/smjer
        ///    {naziv:"",trajanje:100}
        ///
        /// </remarks>
        /// <returns>Kreirani smjer u bazi sa svim podacima</returns>
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

        /// <summary>
        /// Mijenja podatke postojećeg smjera u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/smjer/1
        ///    
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",
        ///  "trajanje": 120,
        ///  "cijena": 890.22,
        ///  "upisnina": 0,
        ///  "verificiran": true
        /// }    
        ///
        /// </remarks>
        /// <param name="sifra">Šifra smjera koji se mijenja</param>  
        /// <returns>Svi poslani podaci od smjera</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
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
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex); 
                // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
            }
        }

        /// <summary>
        /// Briše smjer iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/smjer/1
        ///
        /// </remarks>
        /// <param name="sifra">Šifra smjera koji se briše</param>  
        /// <returns>Odgovor je li obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema smjera kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>  
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]   //ovo se može dodati
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

                return StatusCode(StatusCodes.Status400BadRequest,"Ne moze se obrisati jer smjer ima na sebi grupe");


            }
        }
    }
}
