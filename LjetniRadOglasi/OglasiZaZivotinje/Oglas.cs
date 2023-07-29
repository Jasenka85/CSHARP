using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Oglas
    {
        public int Sifra { get; set; }

        public bool Aktivan { get; set; }

        public Korisnik Korisnik { get; set; }
        public int Kategorija { get; set; }      // 1 = poklanjam, 2 = trazim

        public DateTime DatumObjave { get; set; }

        public string NaslovOglasa { get; set; }

        public string OpisOglasa { get; set; }

        public string VrstaZivotinje { get; set; }

        public string ImeZivotinje { get; set; }

        public string SpolZivotinje { get; set; }   // osim mužjak ili ženka moguće su razne kombinacije, jer ponekad poklanjaju više životinja

        public string DobZivotinje { get; set; }      // može biti npr. 3 mjeseca, 2 godine...

        public string Kastriran { get; set; }   // nije samo "da" ili "ne", može pisati da je mužjak kastriran ali ženka nije i razne kombinacije

        public List<Fotografija> Fotografije { get; set; }   //jedan oglas može imati više fotografija, 

        public List<Poruka> Poruke { get; set; }  //jedan oglas može imati više poruka
       
        public override string ToString()
        {
            return Ucitavanje.OdrediKategoriju(Kategorija) + " : " + VrstaZivotinje + " - " + NaslovOglasa;
        }

    }
}
