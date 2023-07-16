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
            if (Pomocno.dev)
            {
                TestniPodaci();
            }
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
                case 3:
                    PromjenaPolaznika();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjePolaznika();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s polaznicima");
                    break;
            }

        }


        public void PregledPolaznika()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("---------- Polaznici ----------");
            Console.WriteLine("-------------------------------");
            int b = 1;
            foreach (Polaznik polaznik in Polaznici)
            {
                Console.WriteLine("\t{0}. {1}", b++, polaznik);
            }
            Console.WriteLine("-------------------------------");
        }

        private void UnosPolaznika()
        {
            var p = new Polaznik();
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru polaznika: ", "Unos mora biti prirodan broj!");
            p.Ime = Pomocno.UcitajString("Unesite ime polaznika: ", "Ime je obavezno!");
            p.Prezime = Pomocno.UcitajString("Unesite prezime polaznika: ", "Prezime je obavezno!");
            p.Email = Pomocno.UcitajString("Unesite e-mail polaznika: ", "E-mail je obavezan!");
            p.Oib = Pomocno.UcitajString("Unesite OIB polaznika: ", "OIB je obavezan!");
            Polaznici.Add(p);
        }

        

        private void PromjenaPolaznika()
        {
            PregledPolaznika();
            int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj polaznika: ", "Nije dobar odabir!", 1, Polaznici.Count());
            var p = Polaznici[index - 1];
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru polaznika (" + p.Sifra + "): ", "Unos mora biti prirodan broj!");
            p.Ime = Pomocno.UcitajString("Unesite ime polaznika (" + p.Ime + "): ", "Ime je obavezno!");
            p.Prezime = Pomocno.UcitajString("Unesite prezime polaznika (" + p.Prezime + "): ", "Prezime je obavezno!");
            p.Email = Pomocno.UcitajString("Unesite e-mail polaznika (" + p.Email + "): ", "E-mail je obavezan!");
            p.Oib = Pomocno.UcitajString("Unesite OIB polaznika (" + p.Oib + "): ", "OIB je obavezan!");

        }


        private void BrisanjePolaznika()
        {
            PregledPolaznika();
            int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj polaznika: ", "Nije dobar odabir!", 1, Polaznici.Count());
            Polaznici.RemoveAt(index - 1);
        }



        private void TestniPodaci()
        {
            Polaznici.Add(new Polaznik
            {
                Sifra = 1,
                Ime = "Ana",
                Prezime = "Gal",
                Email="agal@gmail.com",
                Oib="33736472822"
            }); 

            Polaznici.Add(new Polaznik
            {
                Sifra = 2,
                Ime = "Marija",
                Prezime = "Zimska",
                Email = "mzimska@gmail.com",
                Oib = "33736472821"
            });

        }

    }
}
