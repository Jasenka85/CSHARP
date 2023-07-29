using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class PosaljiPoruku
    {
        public int Sifra { get; set; }

        public Korisnik Korisnik { get; set; }  

        public Oglas Oglas { get; set; }

        public string ImePosiljatelja { get; set; }

        public string EmailPosiljatelja { get; set; }

        public string Poruka { get; set; }

        public DateTime DatumPoruke { get; set; }
    }
}
