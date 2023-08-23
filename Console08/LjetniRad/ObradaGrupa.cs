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
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                       Izbornik za rad s grupama:                                       *");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregled postojećih grupa");
            Console.WriteLine("\t 2. Unos nove grupe");
            Console.WriteLine("\t 3. Promjena postojeće grupe");
            Console.WriteLine("\t 4. Brisanje grupe");
            Console.WriteLine("\t 5. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");

            switch (Pomocno.ucitajBrojRaspon("\nOdaberite stavku izbornika grupe: ", "Odabir mora biti 1-5", 1, 5))
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
                    Console.WriteLine("\nGotov rad s grupama!\n");
                    break;
            }

        }


        public void PrikaziGrupe()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                                Grupe:                                                  *");
            Console.WriteLine("**********************************************************************************************************");

            if (Grupe.Count() == 0)
            {
                Console.WriteLine("\nNema grupa na listi!\n");

            }
            else
            {

                int b = 1;
                foreach (Grupa grupa in Grupe)
                {
                    Console.WriteLine("\t{0}. ({1})", grupa.Naziv, grupa.Smjer.Naziv);
                    foreach (Polaznik polaznik in grupa.Polaznici)
                    {
                        Console.WriteLine("\t\t {0}", polaznik);
                    }
                }
                Console.WriteLine("**********************************************************************************************************");

            }
        }

        private void UnosNoveGrupe()
        {
            var g = new Grupa();

            g.Sifra = Pomocno.ucitajCijeliBroj("\nUnesite sifru grupe: ", "Unos mora biti prirodan broj!");
            g.Naziv = Pomocno.UcitajString("Unesite naziv grupe: ", "Naziv je obavezan!");
            g.Smjer = PostaviSmjer();   
            g.Polaznici = PostaviPolaznike();
            g.DatumPocetka = Pomocno.ucitajDatum("Unesi datum pocetka grupe u formatu dd.MM.yyyy", "Greska!");
            Grupe.Add(g);
        }

        private Smjer PostaviSmjer()
        {
            Izbornik.ObradaSmjer.PrikaziSmjerove();
            int index = Pomocno.ucitajBrojRaspon("\nOdaberi redni broj smjera: ", "Nije dobar odabir!", 1, Izbornik.ObradaSmjer.Smjerovi.Count());
            return Izbornik.ObradaSmjer.Smjerovi[index - 1];
        }


        private List<Polaznik> PostaviPolaznike()
        {
            List<Polaznik> PolazniciGrupe = new List<Polaznik>();
            while (Pomocno.ucitajBool("Zelite li dodati polaznike grupe? Upisite 'da' ili bilo sto drugo za ne: "))
            {
                int zastavica = 0;
                Izbornik.ObradaPolaznik.PregledPolaznika();

                int index = Pomocno.ucitajBrojRaspon("\nOdaberi redni broj polaznika: ", "Nije dobar odabir!", 1, Izbornik.ObradaPolaznik.Polaznici.Count());
                for (int i = 0; i < PolazniciGrupe.Count(); i++)
                {
                    if (Izbornik.ObradaPolaznik.Polaznici[index - 1].Sifra == PolazniciGrupe[i].Sifra)
                    {
                        Console.WriteLine("\nOvaj polaznik je već na popisu!\n");
                        zastavica++;
                        break;
                    }
                }
                if (zastavica == 0)
                {
                    PolazniciGrupe.Add(Izbornik.ObradaPolaznik.Polaznici[index - 1]);
                    Console.WriteLine("\nPolaznik je uspješno dodan na popis.\n");
                }
            }
            return PolazniciGrupe;

        }

        


        private void PromjenaGrupe()
        {
            if (Grupe.Count() == 0)
            {
                Console.WriteLine("\nNema grupa na listi!\n");
                
            }
            else
            { 
                PrikaziGrupe();
                int index = Pomocno.ucitajBrojRaspon("\nOdaberite redni broj grupe (ili 0 za izlaz) ", "Nije dobar odabir!", 0, Grupe.Count());

                if (index != 0)
                {
                    var p = Grupe[index - 1];
                    Console.WriteLine("\nU nastavku unesite nove podatke ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");
                    p.Sifra = Pomocno.UcitajPromjenuBroja("Unesite sifru grupe (" + p.Sifra + "): ", p.Sifra);
                    p.Naziv = Pomocno.UcitajPromjenuStringa("Unesite naziv grupe (" + p.Naziv + "): ", p.Naziv);
                    Console.WriteLine("Trenutni smjer: {0}", p.Smjer.Naziv);
                    p.Smjer = PostaviSmjer();

                    Console.WriteLine("**********************************************************************************************************");
                    Console.WriteLine("*                                         Trenutni polaznici:                                            *");
                    Console.WriteLine("**********************************************************************************************************");
                    int b = 1;
                    foreach (Polaznik polaznik in p.Polaznici)
                    {
                        Console.WriteLine("{0}. {1}", b++, polaznik);
                    }
                    Console.WriteLine("**********************************************************************************************************");
                    p.Polaznici = PostaviPolaznike();
                    Console.WriteLine("\nGrupa je uspješno promijenjena.\n");
                }
            }
        }

        

        private void BrisanjeGrupe()
        {
            if (Grupe.Count() == 0)
            {
                Console.WriteLine("\nNema grupa na listi!\n");

            }
            else
            {
                PrikaziGrupe();
                int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj grupe ili 0 za izlaz: ", "Nije dobar odabir!", 0, Grupe.Count());
                if (index != 0)
                {
                    Grupe.RemoveAt(index - 1);
                    Console.WriteLine("\nGrupa je uspješno obrisana.\n");
                }
            } 
        }


        

        

        

        

        

        
    }
}
