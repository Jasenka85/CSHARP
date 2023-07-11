using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class Smjer: Entitet
    {
        public string Naziv { get; set; }
        public int Trajanje { get; set; }

        public decimal Cijena { get; set; }

        public decimal Upisnina { get; set; }

        public bool Verificiran { get; set; }


    }
}
