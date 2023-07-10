using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E01Nasljedivanje
{
    internal class Osoba: Object     //Object ne treba pisati
                                     // Klasu Object nasljeđuju sve klase, htjele one to ili ne
                                     //klasa može naslijediti samo jednu klasu
    {

        int brojac;
        protected bool uvjet;
        private string naziv;
        internal DateTime datum;        //DateTime je klasa koju mogu koristiti


        public int Sifra { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public Osoba() { }

        // naša klasa može prepisati, odnosno redefinirati ToString metodu koja postoji na klasi Object
        // to se zove override

        public override string ToString()       //određujemo što će predstavljati Osobu
        {
            return Ime + " " + Prezime;         //sada ako napišemo Console.WriteLine(o); ispisat će nam Pero Perić
        }

        public override bool Equals(object? obj)
        {
            var o = obj as Osoba;   
            return Sifra.Equals(o.Sifra);   // uspoređivat će samo po šifri     
        }           
    }
}


//Nasljeđivanje ima smisla raditi kada imamo više podklasa, ne ako imamo samo jednu nadklasu i podklasu