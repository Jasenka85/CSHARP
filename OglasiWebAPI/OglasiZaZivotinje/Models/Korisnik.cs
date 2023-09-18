using System.ComponentModel.DataAnnotations;

namespace OglasiZaZivotinje.Models
{
    public class Korisnik
    {
        [Key]
        public int Sifra { get; set; }

        [Required]  
        public int Uloga { get; set; }  //0=korisnik, 1=admin, 2=moderator, 3=blokiran

        [Required]
        public string? Ime { get; set; }

        [Required]
        public string? Prezime { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Lozinka { get; set; }

        public string? Mobitel { get; set; }

        public string? Grad { get; set; }
    }
}
