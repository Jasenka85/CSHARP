﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Izbornik
    {
        public ObradaKorisnika ObradaKorisnika;
        public ObradaPoruka ObradaPoruka;
        public ObradaOglasa ObradaOglasa;
        public ObradaCrneListe ObradaCrneListe;
        
        
        public Izbornik() 
        {
            ObradaKorisnika = new ObradaKorisnika(this);
            ObradaPoruka = new ObradaPoruka(this);
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
            Console.WriteLine("\t 3. Poruke");
            Console.WriteLine("\t 4. Crna lista");
            Console.WriteLine("\t 5. Izlaz iz programa");
            Console.WriteLine("**********************************************************************************************************");

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 5!", 1, 5))
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
                    ObradaPoruka.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 4:
                    ObradaCrneListe.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;

                case 5:
                    Console.WriteLine("\nHvala na korištenju! Doviđenja! :)\n");
                    Console.WriteLine("**********************************************************************************************************");
                    break;
                



            }
        }

    }
}
