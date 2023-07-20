using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class CrnaLista
    {
        

        public Korisnik Korisnik { get; set; }

        public string RazlogBlokiranja { get; set; }

        public DateTime DatumBlokiranja { get; set; }

        

        public override string ToString()
        {
            return Korisnik.Ime + " " + Korisnik.Prezime + " - " + RazlogBlokiranja;
        }

    }
    
}
