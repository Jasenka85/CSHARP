using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class Grupa: Entitet
    {
        public string Naziv { get; set; }
        public Smjer Smjer { get; set; }
        public DateTime DatumPocetka { get; set; }
    }
}
