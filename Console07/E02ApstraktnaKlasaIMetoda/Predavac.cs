using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E02ApstraktnaKlasaIMetoda
{
    internal class Predavac: Osoba
    {
        public int Godine { get; set; }
        public override void Pozdravi()     //apstraktnu metodu moramo imati, ali možemo ju pregaziti
        {
            Console.WriteLine(Godine > 24 ? "Dobar dan gospodine" : "Dobar dan mladiću");
        }
    }
}
