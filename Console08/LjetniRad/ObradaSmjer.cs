using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class ObradaSmjer
    {

        public List<Smjer> Smjerovi { get; }
        public ObradaSmjer() 
        { 
            Smjerovi = new List<Smjer>();
            if (Pomocno.dev)
            {
                TestniPodaci();
            }
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad sa smjerovima");
            Console.WriteLine("1. Pregled postojećih smjerova");
            Console.WriteLine("2. Unos novog smjera");
            Console.WriteLine("3. Promjena postojećeg smjera");
            Console.WriteLine("4. Brisanje smjera");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (Pomocno.ucitajBrojRaspon("Odaberite stavku izbornika smjera: ", "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PrikaziSmjerove();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogSmjera();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaSmjera();
                    PrikaziIzbornik();
                    break;
                case 4:
                    if (Smjerovi.Count == 0)
                    {
                        Console.WriteLine("Nema smjerova za brisanje");
                    }
                    else
                    {
                        BrisanjeSmjera();
                    }
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad sa smjerovima");
                    break;
            }

        }

        

        public void PrikaziSmjerove()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("--------- Smjerovi ----------");
            Console.WriteLine("-----------------------------");
            int b = 1;
            foreach (Smjer smjer in Smjerovi)
            {
                Console.WriteLine("\t{0}. {1}", b++, smjer.Naziv);
            }
            Console.WriteLine("-----------------------------");
        }

        private void UnosNovogSmjera()
        {
            var s = new Smjer();
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru smjera: ", "Unos mora biti prirodan broj");
            s.Naziv = Pomocno.UcitajString("Unesite naziv smjera: ", "Unos je obavezan!");
            s.Trajanje = Pomocno.ucitajCijeliBroj("Unesite trajanje smjera u satima: ", "Unos mora biti prirodan broj!");
            s.Cijena = Pomocno.ucitajDecimalniBroj("Unesite cijenu, stavite . za decimalni dio: ", "Unos mora biti pozitivan broj!");
            s.Upisnina = Pomocno.ucitajDecimalniBroj("Unesite upisninu, stavite . za decimalni dio: ", "Unos mora biti pozitivan broj!");
            s.Verificiran = Pomocno.ucitajBool("Smjer verificiran? Unesite 'da' ili bilo sto drugo za ne: ");
            Smjerovi.Add(s);
            //dodajemo u listu, ALI kada izađemo iz programa ti smjerovi će nestati,
            //ostat će samo Web programiranje
            //da bi ostali trajno upisani trebamo bazu podataka
        }

       

        private void PromjenaSmjera()
        {
            PrikaziSmjerove();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj smjera za promjenu: ", "Nije dobar odabir!", 1, Smjerovi.Count());
            var s = Smjerovi[index - 1];
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru smjera (" + s.Sifra + "): ", "Unos mora biti prirodan broj!");
            s.Naziv = Pomocno.UcitajString("Unesite naziv smjera (" + s.Naziv + "): ", "Unos je obavezan!");
            s.Trajanje = Pomocno.ucitajCijeliBroj("Unesite trajanje smjera u satima (" + s.Trajanje + "): ", "Unos mora biti prirodan broj!");
            s.Cijena = Pomocno.ucitajDecimalniBroj("Unesite cijenu, stavite . za decimalni dio (" + s.Cijena + "): ", "Unos mora biti pozitivan broj!");
            s.Upisnina = Pomocno.ucitajDecimalniBroj("Unesite upisninu, stavite . za decimalni dio (" + s.Upisnina + "): ", "Unos mora biti pozitivan broj!");
            s.Verificiran = Pomocno.ucitajBool("Smjer verificiran? Unesite 'da' ili bilo što drugo za ne (" + (s.Verificiran ? "da" : "ne") + "): ");
        }


        private void BrisanjeSmjera()
        {
            PrikaziSmjerove();
            int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj smjera za brisanje: ", "Nije dobar odabir!", 1, Smjerovi.Count());
            Smjerovi.RemoveAt(index - 1);

        }


        private void TestniPodaci()
        {
            Smjerovi.Add(new Smjer()
            {
                Sifra = 1,
                Naziv = "Web programiranje",
                Trajanje = 250,
                Cijena = 1000,
                Upisnina = 50,
                Verificiran = true
            }) ;

            Smjerovi.Add(new Smjer()
            {
                Sifra = 2,
                Naziv = "Java programiranje",
                Trajanje = 130,
                Cijena = 1000,
                Upisnina = 50,
                Verificiran = true
            });

        }
    }
}
