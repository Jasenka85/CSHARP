using System.ComponentModel.DataAnnotations;

namespace OglasiZaZivotinje.Models.DTO
{
    public class KorisnikDTO
    {
        public int Sifra { get; set; }

        
        public int Uloga { get; set; }

        public string? NazivUloge { get; set; }
       

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public string? Email { get; set; }

        //Lozinka se neće prikazivati

        public string? Mobitel { get; set; }

        public string? Grad { get; set; }

    }
}
