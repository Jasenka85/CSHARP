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
        private Izbornik Izbornik;

        public ObradaGrupa()
        {
            Grupe = new List<Grupa>();

        }

        public ObradaGrupa(Izbornik izbornik) :this()   //poziva konstruktor ObradaGrupa()
            {
            this.Izbornik = izbornik;
        }
        public void PrikaziIzbornik()       //ovo je mogla biti metoda u Pomocno, jer sličnu stvar radimo u više klasa
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
                    PrikaziGrupe();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNoveGrupe();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaGrupe();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeGrupe();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s grupama");
                    break;
            }

        }


        public void PrikaziGrupe()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("----------- Grupe -----------");
            Console.WriteLine("-----------------------------");
            int b = 1;
            foreach (Grupa grupa in Grupe)
            {
                Console.WriteLine("\t{0}. ({1})", grupa.Naziv, grupa.Smjer.Naziv);
                foreach (Polaznik polaznik in grupa.Polaznici)
                {
                    Console.WriteLine("\t\t {0}", polaznik);
                }
            }
            Console.WriteLine("-----------------------------");
        }

        private void UnosNoveGrupe()
        {
            var g = new Grupa();

            g.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru grupe: ", "Unos mora biti prirodan broj!");
            g.Naziv = Pomocno.UcitajString("Unesite naziv grupe: ", "Naziv je obavezan!");
            g.Smjer = PostaviSmjer();   
            g.Polaznici = PostaviPolaznike();
            g.DatumPocetka = Pomocno.ucitajDatum("Unesi datum pocetka grupe u formatu dd.MM.yyyy", "Greska!");
            Grupe.Add(g);
        }

        private Smjer PostaviSmjer()
        {
            Izbornik.ObradaSmjer.PrikaziSmjerove();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj smjera: ", "Nije dobar odabir!", 1, Izbornik.ObradaSmjer.Smjerovi.Count());
            return Izbornik.ObradaSmjer.Smjerovi[index - 1];
        }


        private List<Polaznik> PostaviPolaznike()
        {
            List<Polaznik> polaznici = new List<Polaznik>();
            while (Pomocno.ucitajBool("Zelite li dodati polaznike? Upisite 'da' ili bilo sto drugo za ne: "))
            {
                polaznici.Add(PostaviPolaznika());
            }
            return polaznici;

        }

        private Polaznik PostaviPolaznika()
        {
            Izbornik.ObradaPolaznik.PregledPolaznika();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj polaznika: ", "Nije dobar odabir!", 1, Izbornik.ObradaPolaznik.Polaznici.Count());
            return Izbornik.ObradaPolaznik.Polaznici[index - 1];
        }


        private void PromjenaGrupe()
        {
            PrikaziGrupe();
            int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj grupe: ", "Nije dobar odabir!", 1, Grupe.Count());
            var p = Grupe[index - 1];
            p.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru grupe (" + p.Sifra + "): ", "Unos mora biti prirodan broj!");
            p.Naziv = Pomocno.UcitajString("Unesite naziv grupe (" + p.Naziv + "): ", "Unos je obavezan!");
            Console.WriteLine("Trenutni smjer: {0}", p.Smjer.Naziv);
            p.Smjer = PostaviSmjer();
            Console.WriteLine("Trenutni polaznici:");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("--------- Polaznici ---------");
            Console.WriteLine("-----------------------------");
            int b = 1;
            foreach (Polaznik polaznik in p.Polaznici)
            {
                Console.WriteLine("{0}. {1}", b++, polaznik);
            }
            Console.WriteLine("-----------------------------");
            p.Polaznici = PostaviPolaznike();
        }

        

        private void BrisanjeGrupe()
        {
            PrikaziGrupe();
            int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj grupe: ", "Nije dobar odabir!", 1, Grupe.Count());
            Grupe.RemoveAt(index - 1);
        }


        

        

        

        

        

        
    }
}
