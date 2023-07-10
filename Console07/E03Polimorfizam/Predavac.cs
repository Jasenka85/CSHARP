using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E03Polimorfizam
{
    internal class Predavac: Osoba
    {
        public override string Pozdravi()
        {
            return "Dobar dan predavaču " + Ime;
        }
    }
}
