using System.ComponentModel.DataAnnotations;

namespace EdunovaApp.Models
{
    public class Smjer: Entitet
    {
        [Required]
        public string? Naziv { get; set; }

        [Required]
        [Range(30,500)]
        public int Trajanje { get; set; }

        public decimal? Cijena { get; set; }

        public decimal? Upisnina { get; set; }

        public bool? Verificiran { get; set; }
    }
}
