using System.ComponentModel.DataAnnotations;

namespace EdunovaApp.Models
{
    public abstract class Entitet
    {
        [Key]
        public int Sifra { get; set; }

    }
}
