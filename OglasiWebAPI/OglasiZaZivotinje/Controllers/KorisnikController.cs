using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije s entitetom Korisnik u bazi
    /// </summary>
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly OglasiContext _context;
        private readonly ILogger<KorisnikController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public KorisnikController(OglasiContext context, ILogger<KorisnikController> logger)
        {
            _context = context;
            _logger = logger;   
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
        /// <returns>Svi korisnici u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam korisnike...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var korisnici = _context.Korisnik.ToList();

                if (korisnici == null || korisnici.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<KorisnikDTO> prikazi = new();
                korisnici.ForEach(k =>
                {
                    string imeuloge = " ";
                    switch (k.Uloga)
                    {
                        case 0:
                            imeuloge = "korisnik";
                            break;
                        case 1:
                            imeuloge = "administrator";
                            break;
                        case 2:
                            imeuloge = "moderator";
                            break;
                        case 3:
                            imeuloge = "blokiran";
                            break;
                    }
                    
                    prikazi.Add(new KorisnikDTO()
                    {
                        Sifra = k.Sifra,
                        Uloga = k.Uloga,
                        NazivUloge = imeuloge,
                        Ime = k.Ime,
                        Prezime = k.Prezime,
                        Email=k.Email,
                        //lozinka se neće prikazivati
                        Mobitel=k.Mobitel,
                        Grad=k.Grad
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
        /// Dohvaća korisnika sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Korisnik/{sifra}
        ///
        /// </remarks>
        /// <returns>Korisnika sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam korisnika sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return new JsonResult(korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Dohvaća administratore i moderatore
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Korisnik/Admini
        ///
        /// </remarks>
        /// <returns>Popis administratora i moderatora</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("Admini")]
        public IActionResult GetAdmini()
        {
            _logger.LogInformation("Dohvaćam administratore i moderatore...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var korisnici = _context.Korisnik.ToList();

                if (korisnici == null || korisnici.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<KorisnikDTO> prikazi = new();

                foreach(Korisnik k in korisnici)
                {
                    
                    if (k.Uloga == 1 || k.Uloga == 2)
                    {
                        string imeuloge = " ";
                        switch (k.Uloga)
                        {
                            case 1:
                                imeuloge = "administrator";
                                break;
                            case 2:
                                imeuloge = "moderator";
                                break;
                        }
                        prikazi.Add(new KorisnikDTO()
                        {
                            Sifra = k.Sifra,
                            Uloga = k.Uloga,
                            NazivUloge = imeuloge,
                            Ime = k.Ime,
                            Prezime = k.Prezime,
                            Email = k.Email,
                            //lozinka se neće prikazivati
                            Mobitel = k.Mobitel,
                            Grad = k.Grad
                        });
                    }
                };

                if (prikazi.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return new JsonResult(prikazi);
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
        /// Napomena: "sifra" se dohvaća iz baze,
        /// novi korisnik uvijek ima ulogu 0 (običan korisnik)
        /// 
        /// </remarks>
        /// <returns>Kreiranog korisnika u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult Post(KorisnikDTO kDto)
        {
            _logger.LogInformation("Dodajem novog korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (kDto == null)
            {
                return BadRequest();
            }

            try
            {
                Korisnik k = new Korisnik()
                {
                    Uloga = 0,  //novi korisnik je u početku običan korisnik (uloga=0)
                                //samo administrator može promijeniti ulogu i dodati lozinku
                    Ime = kDto.Ime,
                    Prezime = kDto.Prezime,
                    Email = kDto.Email,
                    Lozinka = "",   //lozinka će se popuniti samo ako član postane administrator ili moderator
                    Mobitel = kDto.Mobitel,
                    Grad = kDto.Grad
                };
                
                _context.Korisnik.Add(k);
                _context.SaveChanges();

                kDto.Sifra = k.Sifra; //dohvatim šifru iz baze
                kDto.Uloga = 0;   //pregazim ono što je upisao korisnik
                kDto.NazivUloge = "korisnik";
                return Ok(kDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Mijenja podatke korisnika sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}
        ///    
        /// Parametar: šifra korisnika kojeg želite mijenjati
        ///
        /// Napomena: "sifra" se dohvaća iz baze
        /// Uloga i lozinka se ne mogu mijenjati, to može samo administrator u posebnoj ruti
        /// 
        /// </remarks>
        /// <returns>Promijenjenog korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KorisnikDTO kDto)
        {

            _logger.LogInformation("Mijenjam podatke korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (kDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                
                //uloga se ne mijenja
                korisnik.Ime = kDto.Ime;
                korisnik.Prezime = kDto.Prezime;
                korisnik.Email = kDto.Email;
                //lozinka se ne mijenja
                korisnik.Mobitel = kDto.Mobitel;
                korisnik.Grad = kDto.Grad;

                _context.Korisnik.Update(korisnik);
                _context.SaveChanges();

                kDto.Sifra = korisnik.Sifra;   //dohvatim sifru iz baze
                kDto.Uloga = korisnik.Uloga;   //pregazim ono što je unio korisnik

                string imeuloge = " ";
                switch (korisnik.Uloga)
                {
                    case 0:
                        imeuloge = "korisnik";
                        break;
                    case 1:
                        imeuloge = "administrator";
                        break;
                    case 2:
                        imeuloge = "moderator";
                        break;
                    case 3:
                        imeuloge = "blokiran";
                        break;
                }

                kDto.NazivUloge = imeuloge;

                //lozinka se ne prikazuje

                return Ok(kDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message); 
            }

        }




        /// <summary>
        /// Mijenja ulogu i lozinku korisnika sa zadanom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Korisnik/{sifra}/Uloga
        ///    
        /// Parametri: šifra korisnika, nova uloga i lozinka
        ///
        /// Uloge: 0 = korisnik, 1 = administrator, 2 = moderator
        /// Lozinka je potrebna samo za administratora i moderatora.
        /// 
        /// </remarks>
        /// <returns>Promijenjenog korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("Uloga/{sifra:int}")]
        public IActionResult PromjenaUloge(int sifra, Korisnik k)
        {

            _logger.LogInformation("Mijenjam ulogu korisnika...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (k == null)
            {
                return BadRequest();
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }
            if (k.Uloga < 0 || k.Uloga > 2)
            {
                return new JsonResult("{\"poruka\":\"Uloga korisnika može biti samo 0, 1 ili 2.\"}");
            }
            

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                if (korisnik.Uloga == 3)
                {
                    return new JsonResult("{\"poruka\":\"Korisnik je na crnoj listi!\"}");
                }

                korisnik.Uloga = k.Uloga;
                korisnik.Lozinka = k.Lozinka;

                //pregazim ono što je uneseno
                k.Sifra = korisnik.Sifra;   //dohvatim sifru iz baze
                k.Ime = korisnik.Ime;
                k.Prezime = korisnik.Prezime;
                k.Email = korisnik.Email;
                k.Mobitel = korisnik.Mobitel;
                k.Grad = korisnik.Grad;

                _context.Korisnik.Update(korisnik);
                _context.SaveChanges();

                return Ok(k);

                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
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
        /// Parametar: šifra korisnika kojeg želite obrisati
        /// 
        /// Napomena: nije moguće obrisati administratora i moderatora,
        /// kao ni korisnika koji ima objavljen oglas.
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
            _logger.LogInformation("Brišem korisnika...");

            if (sifra < 1)
            {
                return BadRequest();
            }

            try
            {
                var korisnik = _context.Korisnik.Find(sifra);
                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                if (korisnik.Uloga == 1 || korisnik.Uloga == 2)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Ne mogu obrisati administratora i moderatora.");
                }

                _context.Korisnik.Remove(korisnik);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Korisnik obrisan.\"}");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Korisnik se ne može obrisati jer ima oglas.");
            }
        }


        



    }
}
