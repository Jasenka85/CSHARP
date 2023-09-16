using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models.DTO
{
    public class Crna_listaDTO
    {
        public int Sifra { get; set; }

        public string? Korisnik { get; set; }

        public int Sifra_korisnika { get; set; }

        public string? Razlog_blokiranja { get; set; }

        public DateTime? Datum_blokiranja { get; set; }
    }
}
