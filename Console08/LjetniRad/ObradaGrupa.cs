using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class ObradaGrupa
    {
        public List<Grupa> Grupe { get; }

        public ObradaGrupa()
        {
            Grupe = new List<Grupa>();

        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s grupama");
            Console.WriteLine("1. Pregled postojećih grupa");
            Console.WriteLine("2. Unos nove grupe");
            Console.WriteLine("3. Promjena postojeće grupe");
            Console.WriteLine("4. Brisanje grupe");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika grupe: ", "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledGrupe();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosGrupe();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s grupama");
                    break;
            }

        }

        private void UnosGrupe()
        {
            var g = new Grupa();

            g.Naziv = Pomocno.UcitajString("Unesite naziv grupe: ", "Naziv je obavezan");
            // Smjer se ne mora unositi jer su smjer i grupa povezani?
            // Što je sa šifrom?
            // Kako unijeti datum početka?

            Grupe.Add(g);
        }

        private void PregledGrupe()
        {
            foreach (Grupa grupa in Grupe)
            {
                Console.WriteLine(grupa.Naziv);
            }
        }

        
    }
}
