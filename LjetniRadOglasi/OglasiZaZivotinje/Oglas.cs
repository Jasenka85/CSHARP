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

        public Korisnik Korisnik { get; set; }
        public int Kategorija { get; set; }      // 0 = poklanjam, 1 = trazim

        public DateTime DatumObjave { get; set; }

        public string NaslovOglasa { get; set; }

        public string OpisOglasa { get; set; }

        public string VrstaZivotinje { get; set; }

        public string ImeZivotinje { get; set; }

        public string SpolZivotinje { get; set; }   // osim mužjak ili ženka moguće su razne kombinacije, jer ponekad poklanjaju više životinja

        public string DobZivotinje { get; set; }      // može biti npr. 3 mjeseca, 2 godine...

        public bool Kastriran { get; set; }

        public List<Fotografija> Fotografije { get; set; }   //jedan oglas može imati više fotografija, 

        


    }
}
