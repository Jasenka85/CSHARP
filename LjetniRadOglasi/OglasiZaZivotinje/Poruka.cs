using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Poruka
    {
        public int Sifra { get; set; }

        public string ImePosiljatelja { get; set; }

        public string EmailPosiljatelja { get; set; }

        public string TekstPoruke { get; set; }

        public DateTime DatumPoruke { get; set; }
    }
}
