using System.ComponentModel;

namespace OglasiZaZivotinje.Models.DTO
{
    public class CijeliOglasDTO
    {
        //podaci od korisnika
        public int SifraKorisnika { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public string? Email { get; set; }

        public string? Mobitel { get; set; }

        public string? Grad { get; set; }

        //podaci od oglasa

        public int SifraOglasa { get; set; }

        [DefaultValue(false)]   //oglas je neaktivan dok ga ne odobri administrator
        public bool Aktivan { get; set; }

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
