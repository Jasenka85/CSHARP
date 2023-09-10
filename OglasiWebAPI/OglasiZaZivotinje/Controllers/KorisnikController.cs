using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije sa entitetom Korisnik u bazi
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly OglasiContext _context;

        public KorisnikController(OglasiContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Dohvaća sve korisnike iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Korisnik
        ///
        /// </remarks>
        /// <returns>Korisnici u bazi</returns>
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
                var korisnici = _context.Korisnik.ToList();
                if (korisnici == null || korisnici.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Korisnik.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }


        /// <summary>
        /// Dodaje korisnika u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Korisnik
        ///
        /// </remarks>
        /// <returns>Kreiranog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]

        public IActionResult Post(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Korisnik.Add(korisnik);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Mijenja korisnika sa zadanom šifrom u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}
        ///
        /// </remarks>
        /// <returns>Promijenjenog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]

        public IActionResult Put(int sifra, Korisnik korisnik)
        {
            if (sifra <= 0 || korisnik == null)
            {
                return BadRequest();
            }

            try
            {
                var korisnikBaza = _context.Korisnik.Find(sifra);
                if (korisnikBaza == null)
                {
                    return BadRequest();
                }
                
                korisnikBaza.Uloga = korisnik.Uloga;
                korisnikBaza.Ime = korisnik.Ime;
                korisnikBaza.Prezime = korisnik.Prezime;
                korisnikBaza.Email = korisnik.Email;
                korisnikBaza.Lozinka = korisnik.Lozinka;
                korisnikBaza.Mobitel = korisnik.Mobitel;
                korisnikBaza.Grad = korisnik.Grad;

                _context.Korisnik.Update(korisnikBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, korisnikBaza);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex); 
            }

        }


        /// <summary>
        /// Briše korisnika sa zadanom šifrom iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Korisnik/{sifra}
        ///
        /// </remarks>
        /// <returns>Poruku da je obrisao korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
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
                var korisnikBaza = _context.Korisnik.Find(sifra);
                if (korisnikBaza == null)
                {
                    return BadRequest();
                }
                //napisati provjeru može li se obrisati

                _context.Korisnik.Remove(korisnikBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {

                try
                {
                    SqlException sqle = (SqlException)ex;
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, sqle);
                }
                catch (Exception e)
                {

                }

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
            }

        }


        [HttpGet]
        [Route("DodajKorisnikaRucno")]

        public string UnosUBazu()
        {
            Korisnik k;
            for (int i = 0; i < 1000; i++)
            {
                k = new()
                {
                    Uloga = 0,
                    Ime = "Ime" + i,
                    Prezime = "Prezime" + i,
                    Email = "Email" + i,
                    Lozinka = "",
                    Mobitel = "091654762" + i,
                    Grad = "Grad" + i
                };
                _context.Korisnik.Add(k);
                _context.SaveChanges();
            }
            return "Uneseno 1000 korisnika";
        }


        [HttpGet]
        [Route("DodajKorisnikaFaker")]

        public string PopuniBazu()
        {
            Korisnik k;
            for (int i = 0; i < 1000; i++)
            {
                k = new()
                {
                    Uloga = 0,
                    Ime = Faker.Name.First(),
                    Prezime = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Lozinka = "",
                    Mobitel = "09"+Faker.RandomNumber.Next(1111111,10000000),
                    Grad = Faker.Address.City()
                };
                _context.Korisnik.Add(k);
                _context.SaveChanges();
            }
            return "Uneseno 1000 fake korisnika";
        }

        //Ruta koja na svakom entitetu koji ima parnu šifru jednom atributu
        //mijenja vrijednost i sprema u bazu


        [HttpGet]
        [Route("PromijeniParnog")]

        public string Promjena()
        
        {
            var korisnici = _context.Korisnik.ToList();

            foreach (var k in korisnici)
            {
                if (k.Sifra % 2 == 0)
                {
                    k.Ime += "mijenjao";
                    _context.Korisnik.Update(k);
                }
              
                
            }
            _context.SaveChanges();
            return "Promijenio sam parne korisnike";

        }


        //Napisati rutu koja vraća sumu svih šifri na odabranom entitetu

        [HttpGet]
        [Route("SumaSifri")]

        public int Zbroj()
        {
            var korisnici = _context.Korisnik.ToList();
            int zbroj = 0;
            foreach (var k in korisnici)
            {
                    zbroj += k.Sifra;
            }
            return zbroj;
        }



    }
}
