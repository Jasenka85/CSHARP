using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E02ApstraktnaKlasaIMetoda
{
    internal abstract class Osoba       //apstraktna klasa nije potpuna
    {
        public abstract void Pozdravi();  //ova metoda nema tijelo!
                //tijelo je u nekim podklasama koje su naslijedile Osoba
               // možemo umjesto abstract staviti virtual, tako da ovdje bude definirano tijelo, a u podklasama ga možemo pregaziti
               //ali onda neće forsirati da implentiramo tu metodu
        public int Sifra { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set;}
    }
}
