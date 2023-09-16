using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models.DTO
{
    public class FotografijaDTO
    {

        public int Sifra { get; set; }

       
        public string? Oglas { get; set; }

        public int Sifra_oglasa { get; set; }

        public string? Naziv { get; set; }

        public string? Link { get; set; }


    }
}
