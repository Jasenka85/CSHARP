using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Fotografija
    {
        public int Sifra { get; set; }

        public string Naziv { get; set; }

        public string Link { get; set; }


        public override string ToString()
        {
            return Naziv + ": " + Link;
        }

    }
}
