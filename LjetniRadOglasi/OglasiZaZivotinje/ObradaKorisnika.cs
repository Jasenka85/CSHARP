using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class ObradaKorisnika
    {
        public List<Korisnik> Korisnici { get; }
        public List<CrnaLista> KorisniciNaListi { get; }
        public ObradaKorisnika()
        { 
            Korisnici = new List<Korisnik>();
            KorisniciNaListi= new List<CrnaLista>(); 
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("***** Izbornik za rad s korisnicima *****");
            Console.WriteLine("1. Pregledaj postojeće korisnike");
            Console.WriteLine("2. Unesi novog korisnika");
            Console.WriteLine("3. Promijeni postojećeg korisnika");
            Console.WriteLine("4. Obriši korisnika");
            Console.WriteLine("5. Dodaj admina ili moderatora");
            Console.WriteLine("6. Stavi korisnika na crnu listu");
            Console.WriteLine("7. Povratak na glavni izbornik");
            Console.WriteLine("*****************************************");
            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 7!", 1, 7))
            { 
            case 1:
                    PregledKorisnika();
                    PrikaziIzbornik();
                    break;
            case 2:
                    UnosKorisnika();
                    PrikaziIzbornik();
                    break;
            case 3:
                    PromjenaKorisnika();
                    PrikaziIzbornik();
                    break;
            case 4:
                    BrisanjeKorisnika();
                    PrikaziIzbornik();
                    break;
            case 5:
                    DodajOvlasti();
                    PrikaziIzbornik();
                    break;



            }
        }


        private void PregledKorisnika()
        {
        Console.WriteLine("*****************************************");
        Console.WriteLine("************** Korisnici: ***************");
        Console.WriteLine("*****************************************");
        int broj = 1;
            foreach (Korisnik k in Korisnici)
            {
                Console.WriteLine("\t{0}. {1}", broj++, k);
            }
            Console.WriteLine("**********************************");
        }

        private void UnosKorisnika()
        {
            var k = new Korisnik();
            k.Sifra = Ucitavanje.UcitajCijeliBroj("Unesite šifru korisnika: ", "Unos mora biti prirodan broj.");
            k.Uloga = 0;      // Po defaultu je 0 = obican korisnik, samo admin može naknadno dodijeliti: 1 = admin, 2 = moderator
            k.Lozinka = "";     // Lozinku imaju samo admin i moderator, to upisuje admin kada im dodjeljuje ovlasti
            k.Ime = Ucitavanje.UcitajString("Unesite ime korisnika: ", "Ime je obavezno.");
            k.Prezime = Ucitavanje.UcitajString("Unesite prezime korisnika: ", "Prezime je obavezno.");
            k.Email = Ucitavanje.UcitajString("Unesite e-mail korisnika: ", "E-mail je obavezan.");
            k.Mobitel = Ucitavanje.UcitajString("Unesite broj mobitela korisnika: ", "Broj mobitela je obavezan.");
            k.Grad = Ucitavanje.UcitajString("Unesite grad ili mjesto: ", "Lokacija je obavezna.");
            k.CrnaLista = false; // Samo administrator može staviti true
            Korisnici.Add(k);
        }


        private void PromjenaKorisnika()
        {
            PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
            var k = Korisnici[index - 1];
            k.Sifra = Ucitavanje.UcitajCijeliBroj("Unesite šifru korisnika (" + k.Sifra +"): ", "Unos mora biti prirodan broj.");
            k.Ime = Ucitavanje.UcitajString("Unesite ime korisnika (" + k.Ime + "): ", "Ime je obavezno.");
            k.Prezime = Ucitavanje.UcitajString("Unesite prezime korisnika (" + k.Prezime + "): ", "Prezime je obavezno.");
            k.Email = Ucitavanje.UcitajString("Unesite e-mail korisnika (" + k.Email + "): ", "E-mail je obavezan.");
            k.Mobitel = Ucitavanje.UcitajString("Unesite broj mobitela korisnika (" + k.Mobitel + "): ", "Broj mobitela je obavezan.");
            k.Grad = Ucitavanje.UcitajString("Unesite grad ili mjesto (" + k.Grad + "): ", "Lokacija je obavezna.");
            // ostala svojstva može mijenjati samo administrator preko izbornika
        }


        private void BrisanjeKorisnika()
        {
            PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
            Korisnici.RemoveAt(index - 1);
        }

        private void DodajOvlasti()
        {
            PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
            var k = Korisnici[index - 1];
            k.Uloga = Ucitavanje.UcitajBrojRaspon("Odredite razinu ovlasti: 1 za administratora, 2 za moderatora.", "Treba upisati broj 1 ili 2.", 1, 2);
            k.Lozinka = Ucitavanje.UcitajString("Odredite lozinku za korisnika: ", "Lozinka je obavezna za admina i moderatora.");
        }

        private void NaCrnuListu()
        {
            PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
            var k = Korisnici[index - 1];
            k.CrnaLista = true;
            var cl = new CrnaLista(k);
            cl.IPadresa = Ucitavanje.UcitajString("Upišite IP adresu korisnika: ", "IP adresa je obavezna.");
            cl.RazlogBlokiranja = Ucitavanje.UcitajString("Upišite razlog blokiranja ovog korisnika: ", "Razlog je obavezan.");
            cl.DatumBlokiranja = Ucitavanje.UcitajDatum("Upišite datum blokiranja: ", "Nije dobar format datuma.");
            KorisniciNaListi.Add(cl);

        }

    }
}
