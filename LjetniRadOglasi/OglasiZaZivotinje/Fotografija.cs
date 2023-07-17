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

        public Oglas Oglas { get; set; }

        public string Link { get; set; }    
    }
}
