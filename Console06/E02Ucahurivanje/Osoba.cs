using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E02Ucahurivanje
{
    internal class Osoba
    {
        // Učahurivanje
        // Klasa će sakriti svoja svojstva
        private string ime;

        // klase će učiniti svojstvo dostupnim putem tzv. get i set metoda

        public void setIme(string ime)
        { 
            this.ime = ime;  // kad stavim this.ime mislim na gornji ime, ne ovaj koji je primljen
        }

        public string getIme()
        {
            return this.ime;
        }

        //skraćeno
        // u nastavku ćemo  koristiti ovaj način
        public string Prezime { get; set; }
        public Osoba() 
        { 
        
        }
    }
}
