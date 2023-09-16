using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OglasiZaZivotinje.Models
{
    public class Fotografija
    {
        [Key]
        public int Sifra { get; set; }

        [ForeignKey("oglas")]
        public Oglas? Oglas { get; set; }

        [Required]
        public string? Naziv { get; set; }

        [Required]
        public string? Link { get; set; }




    }
}
