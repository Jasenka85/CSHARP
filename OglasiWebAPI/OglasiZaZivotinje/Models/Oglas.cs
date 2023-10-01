using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models
{
    public class Oglas
    {
        [Key]
        public int Sifra { get; set; }

          
        public bool Aktivan { get; set; }

        [ForeignKey("korisnik")]
        public Korisnik? Korisnik { get; set; }

        [Required(ErrorMessage = "Kategorija je obavezna!")]
        public int Kategorija { get; set; }      // 1 = poklanjam, 2 = trazim


        public DateTime? Datum_objave { get; set; }

        [Required(ErrorMessage = "Naslov oglasa je obavezan!")]
        public string? Naslov { get; set; }

        public string? Opis { get; set; }

        [Required(ErrorMessage = "Vrsta životinje je obavezna!")]
        public string? Vrsta_zivotinje { get; set; }

        public string? Ime_zivotinje { get; set; }

        public string? Spol_zivotinje { get; set; }   // osim mužjak ili ženka moguće su razne kombinacije, jer ponekad poklanjaju više životinja

        public string? Dob_zivotinje { get; set; }      // može biti npr. 3 mjeseca, 2 godine...

        public string? Kastriran { get; set; }   // nije samo "da" ili "ne", može pisati da je mužjak kastriran ali ženka nije i razne kombinacije

        



    }
}
