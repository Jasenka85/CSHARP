using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OglasiZaZivotinje.Models.DTO
{
    public class OglasDTO
    {
        public int Sifra { get; set; }

        [DefaultValue(false)]   //oglas je neaktivan dok ga ne odobri administrator
        public bool Aktivan { get; set; }
        
        public string? Korisnik { get; set; }

        public int Sifra_korisnika { get; set; }
        public int Kategorija { get; set; } 

        public DateTime? Datum_objave { get; set; }

        public string? Naslov { get; set; }

        public string? Opis { get; set; }

        public string? Vrsta_zivotinje { get; set; }

        public string? Ime_zivotinje { get; set; }

        public string? Spol_zivotinje { get; set; }   // osim mužjak ili ženka moguće su razne kombinacije, jer ponekad poklanjaju više životinja

        public string? Dob_zivotinje { get; set; }      // može biti npr. 3 mjeseca, 2 godine...

        public string? Kastriran { get; set; }   // nije samo "da" ili "ne", može pisati da je mužjak kastriran ali ženka nije i razne kombinacije



    }
}
