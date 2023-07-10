using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E02Ucahurivanje
{
    internal class Smjer
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int Trajanje { get; set; }
        public decimal Upisnina { get; set; }
        public bool Certificiran { get; set; }

        public Smjer() { }
    }
}
