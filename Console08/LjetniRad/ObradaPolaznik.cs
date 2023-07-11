using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class ObradaPolaznik
    {
        public List<Polaznik> Polaznici { get; }

        public ObradaPolaznik()
        {
            Polaznici = new List<Polaznik>();
           
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s polaznicima");
            Console.WriteLine("1. Pregled postojećih polaznika");
            Console.WriteLine("2. Unos novog polaznika");
            Console.WriteLine("3. Promjena postojećeg polaznika");
            Console.WriteLine("4. Brisanje polaznika");
            Console.WriteLine("5. Povratak na glavni izbornik");
            switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika polaznika: ", "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledPolaznika();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosPolaznika();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s polaznicima");
                    break;
            }

        }

        private void PregledPolaznika()
        {
            foreach (Polaznik polaznik in Polaznici)
            {
                Console.WriteLine(polaznik);
            }
        }

        private void UnosPolaznika()
        {
            var p = new Polaznik();
            
            p.Ime = Pomocno.UcitajString("Unesite ime polaznika: ", "Ime je obavezno");
            p.Prezime = Pomocno.UcitajString("Unesite prezime polaznika: ", "Prezime je obavezno");
            // ostala svojstva kasnije -> Sifra, Oib, Email

            Polaznici.Add(p);
        }



    }
}
