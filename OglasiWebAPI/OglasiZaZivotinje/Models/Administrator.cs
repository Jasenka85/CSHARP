using System.ComponentModel.DataAnnotations;

namespace OglasiZaZivotinje.Models
{
    public class Administrator
    {
        [Key]
        public int Sifra { get; set; }
        public string? Email { get; set; }
        public string? Lozinka { get; set; }
    }
}
