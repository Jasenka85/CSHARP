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
            TestniPodaci();
 
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
                    break;
                case 5:
                    Console.WriteLine("Gotov rad sa smjerovima");
                    break;
            }



        }

        private void UnosNovogSmjera()
        {
            var s = new Smjer();
            s.Sifra = Pomocno.ucitajCijeliBroj("Unesite sifru smjera: ", "Unos mora biti pozitivni cijeli broj");
            s.Naziv = Pomocno.UcitajString("Unesite naziv smjera: ", "Unos obavezan");
            s.Trajanje = Pomocno.ucitajCijeliBroj("Unesi trajanje smjera u satima: ", "Unos mora biti pozitivan cijeli broj");
            // ostale atribute kasnije -> Cijena, Upisnina, Verificiran
            Smjerovi.Add(s);    
            //dodajemo u listu, ALI kada izađemo iz programa ti smjerovi će nestati,
            //ostat će samo Web programiranje
            //da bi ostali trajno upisani trebamo bazu podataka
        }

        private void PrikaziSmjerove()
        {
            foreach (Smjer smjer in Smjerovi)
            {
                Console.WriteLine(smjer.Naziv);
            }
        }

        private void TestniPodaci()
        {
            Smjerovi.Add(new Smjer() { Naziv = "Web programiranje" });
        }
    }
}
