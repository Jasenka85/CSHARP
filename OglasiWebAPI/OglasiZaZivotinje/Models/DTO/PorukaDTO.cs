using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OglasiZaZivotinje.Models.DTO
{
    public class PorukaDTO
    {
        
        public int Sifra { get; set; }

        
        public string? Oglas { get; set; }

        public int Sifra_oglasa { get; set; }


        public string? Ime_posiljatelja { get; set; }

        
        public string? Email_posiljatelja { get; set; }

        
        public string? Tekst_poruke { get; set; }

        
        public DateTime? Datum_poruke { get; set; }



    }
}
