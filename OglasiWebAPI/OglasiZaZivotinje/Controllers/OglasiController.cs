﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<OglasDTO> prikazi = new();

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                oglasi.ForEach(o =>
                {
                    var putanja = "/slike/nemaslike.png";
                    if(System.IO.File.Exists(dir + o.Sifra + ".png"))
                    {
                        putanja = "/slike/oglasi/" + o.Sifra + ".png";
                    }    

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
                        Kastriran=o.Kastriran,
                        Slika = putanja
                    });
                });
                return new JsonResult(prikazi.OrderByDescending(p => p.Sifra));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }


        /// <summary>
        /// Dohvaća samo aktivne oglase kategorije "Poklanjam", obrnutim redoslijedom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/Poklanjam
        ///
        /// </remarks>
        /// <returns>Oglase kategorije "Poklanjam", obrnutim redoslijedom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("Poklanjam")]
        public IActionResult GetOglasiPoklanjam()
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<CijeliOglasDTO> prikazi = new();

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                foreach (Oglas o in oglasi)
                {
                    if (o.Kategorija == 1 && o.Aktivan == true)
                    {
                        var putanja = "/slike/nemaslike.png";
                        if (System.IO.File.Exists(dir + o.Sifra + ".png"))
                        {
                            putanja = "/slike/oglasi/" + o.Sifra + ".png";
                        }
                        prikazi.Add(new CijeliOglasDTO()
                        {
                            SifraKorisnika = o.Korisnik.Sifra,
                            Ime = o.Korisnik?.Ime,
                            Prezime = o.Korisnik?.Prezime,
                            Email = o.Korisnik?.Email,
                            Mobitel = o.Korisnik?.Mobitel,
                            Grad = o.Korisnik?.Grad,
                            SifraOglasa = o.Sifra,
                            Aktivan = o.Aktivan,
                            Kategorija = o.Kategorija,
                            Datum_objave = o.Datum_objave,
                            Naslov = o.Naslov,
                            Opis = o.Opis,
                            Vrsta_zivotinje = o.Vrsta_zivotinje,
                            Ime_zivotinje = o.Ime_zivotinje,
                            Spol_zivotinje = o.Spol_zivotinje,
                            Dob_zivotinje = o.Dob_zivotinje,
                            Kastriran = o.Kastriran,
                            Slika=putanja
                        });
                    }
                };
                if (prikazi.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }


                return new JsonResult(prikazi.OrderByDescending(p=>p.SifraOglasa));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }



        /// <summary>
        /// Dohvaća samo aktivne oglase kategorije "Tražim", obrnutim redoslijedom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/Trazim
        ///
        /// </remarks>
        /// <returns>Oglase kategorije "Tražim", obrnutim redoslijedom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("Trazim")]
        public IActionResult GetOglasiTrazim()
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                List<CijeliOglasDTO> prikazi = new();

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                foreach (Oglas o in oglasi)
                {
                    if (o.Kategorija == 2 && o.Aktivan == true)
                    {
                        var putanja = "/slike/nemaslike.png";
                        if (System.IO.File.Exists(dir + o.Sifra + ".png"))
                        {
                            putanja = "/slike/oglasi/" + o.Sifra + ".png";
                        }
                        prikazi.Add(new CijeliOglasDTO()
                        {
                            SifraKorisnika = o.Korisnik.Sifra,
                            Ime = o.Korisnik?.Ime,
                            Prezime = o.Korisnik?.Prezime,
                            Email = o.Korisnik?.Email,
                            Mobitel = o.Korisnik?.Mobitel,
                            Grad = o.Korisnik?.Grad,
                            SifraOglasa = o.Sifra,
                            Aktivan = o.Aktivan,
                            Kategorija = o.Kategorija,
                            Datum_objave = o.Datum_objave,
                            Naslov = o.Naslov,
                            Opis = o.Opis,
                            Vrsta_zivotinje = o.Vrsta_zivotinje,
                            Ime_zivotinje = o.Ime_zivotinje,
                            Spol_zivotinje = o.Spol_zivotinje,
                            Dob_zivotinje = o.Dob_zivotinje,
                            Kastriran = o.Kastriran,
                            Slika=putanja
                        });
                    }
                };

                if (prikazi.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return new JsonResult(prikazi.OrderByDescending(p => p.SifraOglasa));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }





        /// <summary>
        /// Dohvaća oglas sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/{sifra}
        ///
        /// </remarks>
        /// <returns>Oglas sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam oglas sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }

            try
            {
                var o = _context.Oglas
                   .Include(c => c.Korisnik)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (o == null || o.Korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                var putanja = "/slike/nemaslike.png";

                if (System.IO.File.Exists(dir + o.Sifra + ".png"))
                {
                    putanja = "/slike/oglasi/" + o.Sifra + ".png";
                }

                var trazen = new OglasDTO()
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
                    Kastriran = o.Kastriran,
                    Slika = putanja
                };

                return new JsonResult(trazen);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }


        /// <summary>
        /// Dohvaća cijeli oglas sa zadanom sifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Oglasi/CijeliOglas/{sifra}
        ///
        /// </remarks>
        /// <returns>Oglas sa zadanom šifrom</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        [Route("CijeliOglas/{sifra:int}")]
        public IActionResult GetCijeliBySifra(int sifra)
        {

            _logger.LogInformation("Dohvaćam oglas sa zadanom šifrom...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }

            try
            {
                var o = _context.Oglas
                   .Include(c => c.Korisnik)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (o == null || o.Korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                var putanja = "/slike/nemaslike.png";

                if (System.IO.File.Exists(dir + o.Sifra + ".png"))
                {
                    putanja = "/slike/oglasi/" + o.Sifra + ".png";
                }

                var prikazi = new CijeliOglasDTO()
                {
                    SifraKorisnika = o.Korisnik.Sifra,
                    Ime = o.Korisnik?.Ime,
                    Prezime = o.Korisnik?.Prezime,
                    Email = o.Korisnik?.Email,
                    Mobitel = o.Korisnik?.Mobitel,
                    Grad = o.Korisnik?.Grad,
                    SifraOglasa = o.Sifra,
                    Aktivan = o.Aktivan,
                    Kategorija = o.Kategorija,
                    Datum_objave = o.Datum_objave,
                    Naslov = o.Naslov,
                    Opis = o.Opis,
                    Vrsta_zivotinje = o.Vrsta_zivotinje,
                    Ime_zivotinje = o.Ime_zivotinje,
                    Spol_zivotinje = o.Spol_zivotinje,
                    Dob_zivotinje = o.Dob_zivotinje,
                    Kastriran = o.Kastriran,
                    Slika=putanja
                };

                return new JsonResult(prikazi);

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
        [Route("Korisnik/{sifra:int}")]
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var oglasikorisnika = new List<Oglas>();

                foreach (Oglas o in oglasi)
                {
                    if (o.Korisnik.Sifra == sifra)
                    { 
                      oglasikorisnika.Add(o); 
                    }
                }

                if (oglasikorisnika == null || oglasikorisnika.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "oglasi" + ds);

                


                var prikazi = new List<OglasDTO>();
                
                oglasikorisnika.ForEach(o =>
                {
                    var putanja = "/slike/nemaslike.png";

                    if (System.IO.File.Exists(dir + o.Sifra + ".png"))
                    {
                        putanja = "/slike/oglasi/" + o.Sifra + ".png";
                    }

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
                        Kastriran = o.Kastriran,
                        Slika=putanja
                    });
                });

                return new JsonResult(prikazi);

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

                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory() + ds + "wwwroot" + ds + "slike" + ds + "oglasi");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                var putanja = Path.Combine(dir + ds + o.Sifra + ".png");

                if (oDto.Slika != "/slike/nemaslike.png")
                {
                    System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(oDto.Slika));
                    oDto.Slika = putanja;   
                }

                return Ok(oDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }



        /// <summary>
        /// Dodaje novog korisnika i oglas u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Oglasi/CijeliOglas
        ///    
        ///
        /// Napomena: "sifraKorisnika" i "sifraOglasa" se dohvaćaju iz baze, 
        /// "datum_objave" se bilježi automatski,
        /// oglas je neaktivan (false) dok ga ne odobri administrator.
        /// 
        /// Kategorije oglasa: 1 = poklanjam životinju, 2 = želim udomiti životinju.
        /// 
        /// 
        /// </remarks>
        /// <returns>Kreiranog korisnika i oglas u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        [Route("CijeliOglas")]
        public IActionResult PostCO(CijeliOglasDTO coDto)
        {
            _logger.LogInformation("Dodajem novi oglas...");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (coDto == null)
            {
                return BadRequest();
            }
            
            if (coDto.Kategorija < 1 || coDto.Kategorija > 2)
            {
                return new JsonResult("{\"poruka\":\"Kategorija može imati vrijednost 1 ili 2.\"}");
            }

            try
            {
                Korisnik k = new Korisnik()
                {
                    Uloga = 0,  // novi korisnik uvijek ima ulogu 0 = korisnik
                    Ime = coDto.Ime,
                    Prezime = coDto.Prezime,
                    Email = coDto.Email,
                    Mobitel = coDto.Mobitel,
                    Grad = coDto.Grad
                };
                _context.Korisnik.Add(k);
                _context.SaveChanges();
                coDto.SifraKorisnika = k.Sifra; //pregazim ono što je unio korisnik

                Oglas o = new Oglas()
                {
                    Aktivan = false,    //Novi oglas je neaktivan dok ga ne odobri administrator
                    Korisnik = k,
                    Kategorija = coDto.Kategorija,
                    Datum_objave = DateTime.Now,    //Datum se automatski bilježi
                    Naslov = coDto.Naslov,
                    Opis = coDto.Opis,
                    Vrsta_zivotinje = coDto.Vrsta_zivotinje,
                    Ime_zivotinje = coDto.Ime_zivotinje,
                    Spol_zivotinje = coDto.Spol_zivotinje,
                    Dob_zivotinje = coDto.Dob_zivotinje,
                    Kastriran = coDto.Kastriran
                };
                _context.Oglas.Add(o);
                _context.SaveChanges();

                coDto.Aktivan = false;                 //pregazim ono što je unio korisnik
                coDto.Datum_objave = o.Datum_objave;  //pregazim ono što je unio korisnik
                coDto.SifraOglasa = o.Sifra;   //dohvatim šifru iz baze


                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory() + ds + "wwwroot" + ds + "slike" + ds + "oglasi");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                var putanja = Path.Combine(dir + ds + o.Sifra + ".png");

                if (coDto.Slika != "/slike/nemaslike.png")
                {
                    System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(coDto.Slika));
                    coDto.Slika = putanja;
                }

                return Ok(coDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Oglas se ne može objaviti, obavezna polja ne mogu biti prazna.");
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
        /// 
        /// Kategorije oglasa: 1 = poklanjam životinju, 2 = želim udomiti životinju.
        /// 
        /// Napomena: "korisnik" i "sifra korisnika" se dohvaćaju iz baze, 
        /// "datum_objave" se bilježi automatski pri objavi i ne može se mijenjati.
        /// 
        /// </remarks>
        /// <returns>Promijenjeni oglas u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("{sifra:int}")]
        [Authorize]
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
            if (sifra < 1)
            {
                return new JsonResult("{\"poruka\":\"Šifra oglasa ne može biti manja od 1.\"}");
            }
            if (oDto.Kategorija < 1 || oDto.Kategorija > 2)
            {
                return new JsonResult("{\"poruka\":\"Kategorija može imati vrijednost 1 ili 2.\"}");
            }

            try
            {
              
                var oglas = _context.Oglas
                   .Include(c => c.Korisnik)
                   .FirstOrDefault(x => x.Sifra == sifra);

                if (oglas == null || oglas.Korisnik == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                oglas.Aktivan = oDto.Aktivan;
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
                oDto.Korisnik = oglas.Korisnik.Ime + " " + oglas.Korisnik.Prezime;
                oDto.Sifra_korisnika = oglas.Korisnik.Sifra;


                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory() + ds + "wwwroot" + ds + "slike" + ds + "oglasi");
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                var putanja = Path.Combine(dir + ds + oglas.Sifra + ".png");


                if (oDto.Slika == null || oDto.Slika == "")
                {
                    if (System.IO.File.Exists(dir + oglas.Sifra + ".png"))
                    {
                        oDto.Slika = putanja;
                    }
                    else
                    {
                        oDto.Slika = "/slike/nemaslike.png";
                    }  
                }
                else
                {
                    System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(oDto.Slika));
                    oDto.Slika = putanja;
                }

                return Ok(oDto);            
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
        [Authorize]
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
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                
                _context.Oglas.Remove(oglas);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Oglas obrisan.\"}");

            }
            catch (Exception)
            {
                return new JsonResult("{\"poruka\":\"Oglas nije obrisan.\"}");

            }

        }







    }
}
