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
        public ObradaOglasa ObradaOglasa;
        public ObradaCrneListe ObradaCrneListe;
        
        
        public Izbornik() 
        {
            ObradaKorisnika = new ObradaKorisnika();
            ObradaOglasa = new ObradaOglasa(this);
            ObradaCrneListe = new ObradaCrneListe(this);
            
            PozdravnaPoruka();
            PrikaziIzbornik();
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                 Oglasi za zivotinje Console APP v 1.0                                  *");
            
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                             Glavni izbornik                                            *");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Korisnici");
            Console.WriteLine("\t 2. Oglasi");
            Console.WriteLine("\t 3. Crna lista");
            Console.WriteLine("\t 4. Izlaz iz programa");
            Console.WriteLine("**********************************************************************************************************");

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 4!", 1, 4))
            {
                case 1:
                    ObradaKorisnika.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 2:
                    ObradaOglasa.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 3:
                    ObradaCrneListe.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 4:
                    Console.WriteLine("\nHvala na korištenju! Doviđenja! :)\n");
                    Console.WriteLine("**********************************************************************************************************");
                    break;
                



            }
        }

    }
}
