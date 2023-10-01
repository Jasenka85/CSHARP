using System.ComponentModel.DataAnnotations;

namespace EdunovaApp.Models
{
    public class Smjer: Entitet
    {
        [Required(ErrorMessage="Naziv je obavezan")]
        public string? Naziv { get; set; }

        [Required]
        [Range(30,500, ErrorMessage ="{0} mora biti između {1} i {2}")]
        public int Trajanje { get; set; }

        [Range(0,10000, ErrorMessage ="Vrijednost {0} mora biti veća od {1} i manja od {2}")]
        public decimal? Cijena { get; set; }

        public decimal? Upisnina { get; set; }

        public bool? Verificiran { get; set; }
    }
}
