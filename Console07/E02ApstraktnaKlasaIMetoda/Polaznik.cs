using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E02ApstraktnaKlasaIMetoda
{
    internal class Polaznik: Osoba      //Polaznik nasljeđuje apstraktnu klasu koja ima apstraktnu metodu i mora ju implementirati (inače nam javlja grešku)
                                        // Drugi način je da i ova klasa postane apstraktna
    {
        public string BrojUgovora { get; set; }

        public string Spol { get; set; }

        public override void Pozdravi()
        {
            Console.WriteLine(Spol.Equals("Ženski") ? "Dobar dan gospođo" : "Dobar dan gospodine");
        }

    }
}
