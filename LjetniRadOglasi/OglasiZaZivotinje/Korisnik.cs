using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Korisnik
    {
        public int Sifra { get; set; }
        public int Uloga { get; set; }      // po defaultu 0 = obican korisnik, 1 = admin, 2 = moderator, 3 = blokiran (samo admin može postaviti 1, 2 ili 3)

        public string Ime { get; set; }

        public string Prezime { get; set; } 
        
        public string Email { get; set;}

        public string Lozinka { get; set;} 

        public string Mobitel { get; set; }

        public string Grad { get; set; }

        
        

        public override string ToString()
        {   
            return Ime + " " + Prezime + " - " + Ucitavanje.OdrediUlogu(Uloga);
        }



    }

    
}

