using System.ComponentModel.DataAnnotations.Schema;

namespace EdunovaApp.Models
{
    public class Grupa: Entitet
    {
        public string? Naziv { get; set; }

        [ForeignKey("smjer")]
        public Smjer? Smjer { get; set; }

        public DateTime? DatumPocetka { get; set; }

        public List<Polaznik> Polaznici { get; set; } = new();   //da se osiguramo da lista ne bude null
    }
}
