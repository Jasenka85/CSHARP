using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E01KlasaObjekt
{
    

    internal class Osoba
    {
        // ovako se ne smiju deklarirati svojstva u klasi
        // zato što nije zadovoljen OOP princip učahurivanja
        /*
        public string ime;     //ako ne napišemo ništa, smatra se da je private i neće biti vidljivo u Program.cs
        internal string prezime;
        public int godine;
        */
        
        //zadnja vrsta metoda, konstruktor
        // naziv konstruktora mora biti identičan nazivu klase
        // imena klasa počinju velikim slovima
        //poziva se u trenutku instanciranja novog objekta (ključna riječ new)
        // ona nije obavezna, zbog nasljeđivanja
        // ako nije definirana, poziva se konstruktor iz klase iznad? vidi materijale
        public Osoba()
        {
            Console.WriteLine("Konstruktor klase Osoba");

        }

        public Osoba(string ime)
        {
            Console.WriteLine(ime);
        }
    }
}
