using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E01Nasljedivanje
{
    internal class Polaznik : Osoba    //naša klasa nasljeđuje svojstva i metode od klase Osoba
                                       //sada se bavimo samo s onim što je specifično za klasu Polaznik
    {
        public string BrojUgovora { get; set; }


        public override string ToString()
        {
            // vidimo iz nadklase protected, internal i private načine pristupa
            // base.uvjet = true;
            return base.ToString() + " " + BrojUgovora;
        }
    }
}

