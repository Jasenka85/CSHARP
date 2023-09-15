using EdunovaApp.Data;
using EdunovaApp.Models;
using EdunovaApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EdunovaApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetom polaznik u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PolaznikController: ControllerBase
    {
        
        
        private readonly EdunovaContext _context;

        public PolaznikController(EdunovaContext context)
        { 
            _context = context; 
        }

        /// <summary>
        /// Dohvaća sve polaznike iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Polaznik
        ///
        /// </remarks>
        /// <returns>Polaznici u bazi</returns>
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
            var polaznici = _context.Polaznik.ToList();
            if (polaznici == null || polaznici.Count == 0)
            { 
                return new EmptyResult();
            }
            List<PolaznikDTO> vrati = new();

            polaznici.ForEach(p =>
            {
                //ovo je ručno presipavanje, kasnije ćemo upogoniti automapper
                var pdto = new PolaznikDTO()
                {
                    Sifra = p.Sifra,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Oib = p.Oib,
                    Email = p.Email
                };
                vrati.Add(pdto);    
                //sad smo presipali podatke da nam ne ispisuje grupe
            });
            return Ok(vrati);
            //vraća status 200 i listu vrati
            

        }


        /// <summary>
        /// Dodaje polaznika u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Polaznik
        ///    {Ime:"",Prezime:""}
        ///
        /// </remarks>
        /// <returns>Kreirani polaznik u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]
        public IActionResult Post(PolaznikDTO dto)
        {
            if (!ModelState.IsValid)
            { 
            return BadRequest(ModelState);
            }

            try
            {
                Polaznik p = new Polaznik()
                {
                    Ime = dto.Ime,
                    Prezime = dto.Prezime,
                    Oib = dto.Oib,
                    Email = dto.Email

                };

                _context.Polaznik.Add(p);
                _context.SaveChanges();
                dto.Sifra = p.Sifra;
                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);

            }
        }


        /// <summary>
        /// Mijenja podatke postojećeg polaznika u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Polaznik/1
        ///
        /// {
        ///   "sifra": 0,
        ///   "ime": "string",
        ///   "prezime": "string",
        ///   "oib": "string",
        ///   "email": "string"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra polaznika koji se mijenja</param>  
        /// <returns>Svi poslani podaci od polaznika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi polaznika kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, PolaznikDTO pdto)
        {
            if (sifra <= 0 || pdto == null)
            {
                return BadRequest();
            }

            try
            {
                var polaznikBaza = _context.Polaznik.Find(sifra);
                if (polaznikBaza == null)
                {
                    return BadRequest();
                }
                // inače se rade Mapper-i
                // mi ćemo za sada ručno
                polaznikBaza.Ime = pdto.Ime;
                polaznikBaza.Prezime=pdto.Prezime;
                polaznikBaza.Oib= pdto.Oib; 
                polaznikBaza.Email= pdto.Email;

                _context.Polaznik.Update(polaznikBaza);
                _context.SaveChanges();
                pdto.Sifra = polaznikBaza.Sifra;

                return StatusCode(StatusCodes.Status200OK, pdto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
                // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
            }
        }

        /// <summary>
        /// Briše polaznika iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Polaznik/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra polaznika koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi polaznika kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
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
                var polaznikBaza = _context.Polaznik.Find(sifra);
                if (polaznikBaza == null)
                {
                    return BadRequest();
                }

                _context.Polaznik.Remove(polaznikBaza);
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
