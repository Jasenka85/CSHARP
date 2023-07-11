using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class Polaznik: Entitet
    {
        public  string Ime { get; set; }
        public string Prezime { get; set; }
        public string Oib { get; set;}
        public string Email { get; set; }

        //treba li polazniku dodati svojstvo Grupa, tipa Grupa?
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
