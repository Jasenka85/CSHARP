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
        public ObradaCrneListe ObradaCrneListe;
        
        public Izbornik() 
        {
            ObradaKorisnika = new ObradaKorisnika();
            ObradaCrneListe = new ObradaCrneListe(this);
            PozdravnaPoruka();
            PrikaziIzbornik();
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("*******************************************************");
            Console.WriteLine("*        Oglasi za zivotinje Console APP v 1.0        *");
            
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("*******************************************************");
            Console.WriteLine("*                    Glavni izbornik                  *");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("1. Korisnici");
            Console.WriteLine("2. Oglasi");
            Console.WriteLine("3. Pošalji poruku korisniku");
            Console.WriteLine("4. Crna lista");
            Console.WriteLine("5. Izlaz iz programa");
            Console.WriteLine("*******************************************************");

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 5!", 1, 5))
            {
                case 1:
                    ObradaKorisnika.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 4:
                    ObradaCrneListe.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 5:
                    Console.WriteLine("\n Hvala na korištenju! Doviđenja! :)\n");
                    Console.WriteLine("*******************************************************");
                    break;
                



            }
        }

    }
}
