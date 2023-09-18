using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;
using OglasiZaZivotinje.Models;
using OglasiZaZivotinje.Models.DTO;

namespace OglasiZaZivotinje.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije sa entitetom Oglasi u bazi
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class OglasiController: ControllerBase
    {
            private readonly OglasiContext _context;
            private readonly ILogger<OglasiController> _logger;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public OglasiController(OglasiContext context, ILogger<OglasiController> logger)
            {
                _context = context;
                _logger = logger;
            }

        /// <summary>
        /// Dohvaća sve oglase iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi
        ///
        /// </remarks>
        /// <returns>Sve oglase u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam oglase...");

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
                    return new JsonResult("{\"poruka\":\"Nema oglasa na listi.\"}");
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

 


        /// <summary>
        /// Dohvaća sve oglase od traženog korisnika
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/{sifra}/Korisnik
        ///
        /// Parametar: šifra korisnika čije oglase želite pregledati
        /// 
        /// </remarks>
        /// <returns>Sve oglase od traženog korisnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}/Korisnik")]
        public IActionResult OglasiKorisnika(int sifra)
        {
            _logger.LogInformation("Dohvaćam oglase korisnika...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }

            try
            {
                var oglasi = _context.Oglas
                    .Include(k => k.Korisnik)
                    .ToList();

                if (oglasi == null || oglasi.Count == 0)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa na listi.\"}");
                }

                var oglasikorisnika = new List<Oglas>();

                foreach (Oglas o in oglasi)
                {
                    if (o.Korisnik.Sifra == sifra)
                    { 
                      oglasikorisnika.Add(o); 
                    }
                }

                if (oglasikorisnika.Count == 0)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa od tog korisnika.\"}");
                }

                var prikazi = new List<OglasDTO>();
                
                oglasikorisnika.ForEach(o =>
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
                        Vrsta_zivotinje = o.Vrsta_zivotinje,
                        Ime_zivotinje = o.Ime_zivotinje,
                        Spol_zivotinje = o.Spol_zivotinje,
                        Dob_zivotinje = o.Dob_zivotinje,
                        Kastriran = o.Kastriran
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
        /// Dodaje novi oglas u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Oglasi
        ///    
        ///
        /// Napomena: "sifra" i "korisnik" se dohvaćaju iz baze, 
        /// "datum_objave" se bilježi automatski,
        /// oglas je neaktivan (false) dok ga ne odobri administrator.
        /// 
        /// Potrebno je unijeti šifru korisnika koji objavljuje oglas (sifra_korisnika)
        /// 
        /// Kategorije oglasa: 1 = poklanjam životinju, 2 = želim udomiti životinju.
        /// 
        /// 
        /// </remarks>
        /// <returns>Kreirani oglas u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
 
        [HttpPost]
        public IActionResult Post(OglasDTO oDto)
        {
            _logger.LogInformation("Dodajem novi oglas...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (oDto == null)
            {
                return BadRequest();
            }
            if (oDto.Sifra_korisnika < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra korisnika ne može biti manja od 1.\"}");
            }
            if (oDto.Kategorija < 1 || oDto.Kategorija > 2)
            {
                return new JsonResult("{\"poruka\":\"Kategorija može imati vrijednost 1 ili 2.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(oDto.Sifra_korisnika);

                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }
                if (korisnik.Uloga == 3)
                {
                    return new JsonResult("{\"poruka\":\"Korisnik je na crnoj listi i ne može objaviti oglas.\"}");
                }

                Oglas o = new Oglas()
                {
                    Aktivan = false,    //Novi oglas je neaktivan dok ga ne odobri administrator
                    Korisnik = korisnik,
                    Kategorija = oDto.Kategorija,
                    Datum_objave = DateTime.Now,    //Datum se automatski bilježi
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

                oDto.Aktivan = false;                 //pregazim ono što je unio korisnik
                oDto.Datum_objave = o.Datum_objave;  //pregazim ono što je unio korisnik
                oDto.Sifra = o.Sifra;   //dohvatim šifru iz baze
                oDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(oDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }



        /// <summary>
        /// Mijenja oglas sa zadanom šifrom u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Oglasi/{sifra}
        ///
        /// Parametar: šifra oglasa kojeg želite mijenjati
        /// 
        /// Potrebno je unijeti i šifru korisnika koji je objavio oglas (sifra_korisnika).
        /// 
        /// Kategorije oglasa: 1 = poklanjam životinju, 2 = želim udomiti životinju.
        /// 
        /// Napomena: "korisnik" se dohvaća iz baze, 
        /// "datum_objave" se bilježi automatski pri objavi i ne može se mijenjati.
        /// 
        /// </remarks>
        /// <returns>Promijenjeni oglas u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, OglasDTO oDto)
        {
            _logger.LogInformation("Mijenjam oglas...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (oDto == null)
            {
                return BadRequest();
            }
            if (sifra < 1 || oDto.Sifra_korisnika < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra ne može biti manja od 1.\"}");
            }
            if (oDto.Kategorija < 1 || oDto.Kategorija > 2)
            {
                return new JsonResult("{\"poruka\":\"Kategorija može imati vrijednost 1 ili 2.\"}");
            }

            try
            {
                var korisnik = _context.Korisnik.Find(oDto.Sifra_korisnika);
                
                if (korisnik == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema korisnika s tom šifrom.\"}");
                }

                var oglas = _context.Oglas.Find(sifra);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }

                oglas.Aktivan = oDto.Aktivan;
                oglas.Korisnik = korisnik;
                oglas.Kategorija = oDto.Kategorija;
                //datum objave se ne može mijenjati
                oglas.Naslov = oDto.Naslov;
                oglas.Opis = oDto.Opis;
                oglas.Vrsta_zivotinje = oDto.Vrsta_zivotinje;
                oglas.Ime_zivotinje = oDto.Ime_zivotinje;
                oglas.Spol_zivotinje = oDto.Spol_zivotinje;
                oglas.Dob_zivotinje = oDto.Dob_zivotinje;
                oglas.Kastriran = oDto.Kastriran;

                _context.Oglas.Update(oglas);
                _context.SaveChanges();

                oDto.Datum_objave = oglas.Datum_objave;     //pregazim ono što je unio korisnik
                oDto.Sifra = sifra;
                oDto.Korisnik = korisnik.Ime + " " + korisnik.Prezime;
                return Ok(oDto);            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }



        /// <summary>
        /// Aktiviranje oglasa
        /// </summary>
        /// <remarks>
        /// Služi administratoru za brzo odobravanje oglasa
        /// (neaktivni oglasi nisu vidljivi na web stranici)
        /// 
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/{sifra}/Aktiviraj
        ///
        /// </remarks>
        /// <returns>Obavijest da je oglas aktiviran</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}/Aktiviraj")]
        public IActionResult Aktiviraj(int sifra)
        {
            _logger.LogInformation("Aktiviram oglas...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }

            try
            {
                var oglas = _context.Oglas.Find(sifra);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }
                if (oglas.Aktivan == true)
                {
                    return new JsonResult("{\"poruka\":\"Oglas je već aktivan.\"}");
                }

                oglas.Aktivan = true;
                _context.Oglas.Update(oglas);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Oglas je sada aktivan.\"}");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }





        /// <summary>
        /// Briše oglas sa zadanom šifrom iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Oglasi/{sifra}
        ///
        /// Parametar: šifra oglasa kojeg želite obrisati
        /// 
        /// </remarks>
        /// <returns>Poruku da je obrisao oglas</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            _logger.LogInformation("Brišem oglas...");

            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }

            try
            {
                var oglas = _context.Oglas.Find(sifra);

                if (oglas == null)
                {
                    return new JsonResult("{\"poruka\":\"Nema oglasa s tom šifrom.\"}");
                }
                
                _context.Oglas.Remove(oglas);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Oglas obrisan.\"}");

            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Oglas se ne može obrisati.\"}");

            }

        }






    }
}
