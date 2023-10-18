using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models
{
    public class Crna_lista
    {
        [Key]
        public int Sifra { get; set; }

        [ForeignKey("korisnik")]
        public Korisnik? Korisnik { get; set; }

        [Required(ErrorMessage = "Razlog blokiranja je obavezan!")]
        public string? Razlog_blokiranja { get; set; }

        public DateTime? Datum_blokiranja { get; set; }
    }
}
