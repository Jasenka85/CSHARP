using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Izbornik
    {
        public ObradaKorisnika ObradaKorisnika { get; }
        
        public Izbornik() 
        {
            ObradaKorisnika = new ObradaKorisnika();
            PozdravnaPoruka();
            PrikaziIzbornik();
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("* Oglasi za zivotinje Console APP v 1.0 *");
            Console.WriteLine("*****************************************");
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("*             Glavni izbornik           *");
            Console.WriteLine("*****************************************");
            Console.WriteLine("1. Korisnici");
            Console.WriteLine("2. Oglasi");
            Console.WriteLine("3. Pošalji poruku korisniku");
            Console.WriteLine("4. Izlaz iz programa");
            Console.WriteLine("*****************************************");

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 4!", 1, 4))
            {
                case 1:
                    ObradaKorisnika.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                
                case 4:
                    Console.WriteLine("Hvala na korištenju! Doviđenja! :)");
                    break;
                



            }
        }

    }
}
