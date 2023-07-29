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
            Console.WriteLine("*************************************");
            Console.WriteLine("Izbornik za rad s polaznicima");
            Console.WriteLine("1. Pregled postojećih polaznika");
            Console.WriteLine("2. Unos novog polaznika");
            Console.WriteLine("3. Promjena postojećeg polaznika");
            Console.WriteLine("4. Brisanje polaznika");
            Console.WriteLine("5. Povratak na glavni izbornik");
            Console.WriteLine("*************************************");
            switch (Pomocno.ucitajBrojRaspon("\nOdaberite stavku izbornika polaznika: ", "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledPolaznika();
                    PrikaziDetalje();
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

            if (Polaznici.Count() == 0)
            {
                Console.WriteLine("\nNema polaznika na listi!\n");
                PrikaziIzbornik();
            }
            else
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
        }


        private void PrikaziDetalje()
        {

            while (true)
            {
                if (Pomocno.ucitajBool("Želite li detalje o nekom polazniku? Upišite 'da' ili bilo što drugo za ne: "))
                {
                    int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj polaznika kojeg želite pregledati: ", "Nije dobar odabir.", 1, Polaznici.Count());
                    var p = Polaznici[index - 1];

                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("\t Sifra: {0}", p.Sifra);
                    Console.WriteLine("\t Ime: {0}", p.Ime);
                    Console.WriteLine("\t Prezime: {0}", p.Prezime);
                    Console.WriteLine("\t OIB: {0}", p.Oib);
                    Console.WriteLine("\t E-mail: {0}", p.Email);
                    

                }
                else
                {
                    break;
                }
            }

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
            if (Polaznici.Count() == 0)
            {
                Console.WriteLine("\nNema polaznika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledPolaznika();
                int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj polaznika kojeg želite promijeniti (ili 0 za izlaz): ", "Nije dobar odabir!", 0, Polaznici.Count());

                if (index == 0)
                {
                    Console.WriteLine("\nPolaznik nije promijenjen.\n");

                }
                else
                {
                    var p = Polaznici[index - 1];

                    Console.WriteLine("\nU nastavku unesite nove podatke ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");
                    p.Sifra = Pomocno.UcitajPromjenuBroja("Unesite novu sifru polaznika ili enter (" + p.Sifra + "): ", p.Sifra);
                    p.Ime = Pomocno.UcitajPromjenuStringa("Unesite novo ime polaznika ili enter (" + p.Ime + "): ", p.Ime);
                    p.Prezime = Pomocno.UcitajPromjenuStringa("Unesite novo prezime polaznika ili enter (" + p.Prezime + "): ", p.Prezime);
                    p.Email = Pomocno.UcitajPromjenuStringa("Unesite novi e-mail polaznika ili enter (" + p.Email + "): ", p.Email);
                    p.Oib = Pomocno.UcitajPromjenuStringa("Unesite novi OIB polaznika ili enter (" + p.Oib + "): ", p.Oib);
                    Console.WriteLine("\nPolaznik je uspješno promijenjen.\n");
                }

            }
        }

        private void BrisanjePolaznika()
        {
            if (Polaznici.Count() == 0)
            {
                Console.WriteLine("\nNema polaznika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledPolaznika();
                int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj polaznika kojeg želite obrisati (ili 0 za izlaz): ", "Nije dobar odabir!", 0, Polaznici.Count());

                if (index == 0)
                {
                    Console.WriteLine("\nPolaznik nije obrisan.\n");
                }

                else
                {
                    Polaznici.RemoveAt(index - 1);
                    Console.WriteLine("\nPolaznik je uspješno obrisan.\n");
                }
            }
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
