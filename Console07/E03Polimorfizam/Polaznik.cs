using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E03Polimorfizam
{
    internal class Polaznik: Osoba
    {
        public override string Pozdravi()
        {
            return "Dobar dan polazniče " + Ime;
        }
    }
}
