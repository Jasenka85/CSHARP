using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models
{
    public class Poruka
    {
        [Key]
        public int Sifra { get; set; }

        [ForeignKey("oglas")]
        public Oglas? Oglas { get; set; }

        [Required]
        public string? Ime_posiljatelja { get; set; }

        [Required]
        public string? Email_posiljatelja { get; set; }

        [Required]
        public string? Tekst_poruke { get; set; }

        [Required]
        public DateTime? Datum_poruke { get; set; }


    }
}
