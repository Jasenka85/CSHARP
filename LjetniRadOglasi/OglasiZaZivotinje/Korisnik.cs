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
        public int Uloga { get; set; }      // po defaultu 0 = obican korisnik, 1 = admin, 2 = moderator (samo admin može postaviti 1 ili 2)

        public string Ime { get; set; }

        public string Prezime { get; set; } 
        
        public string Email { get; set;}

        public string Lozinka { get; set;} 

        public string Mobitel { get; set; }

        public string Grad { get; set; }

        public string IPadresa { get; set; }     // sustav bi automatski trebao zabilježiti IP adresu
        public bool CrnaLista { get; set; }         // po defaultu false, samo admin može promijeniti


        public override string ToString()
        {
            string s;
            switch (Uloga)
            {
                case 0:
                    s = "korisnik";
                    break;
                case 1:
                    s = "administrator";
                    break;
                case 2:
                    s = "moderator";
                    break;
                default: 
                    s = "";
                    break;
            }
            
            return Ime + " " + Prezime + " " + s;
        }



    }

    
}

