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
            TestniPodaci();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("      Izbornik za rad s korisnicima      ");
            Console.WriteLine("*****************************************");
            Console.WriteLine("1. Pregledaj postojeće korisnike");
            Console.WriteLine("2. Unesi novog korisnika");
            Console.WriteLine("3. Promijeni postojećeg korisnika");
            Console.WriteLine("4. Obriši korisnika");
            Console.WriteLine("5. Dodaj admina ili moderatora");
            Console.WriteLine("6. Pregledaj crnu listu");
            Console.WriteLine("7. Stavi korisnika na crnu listu");
            Console.WriteLine("8. Obriši korisnika iz crne liste");
            Console.WriteLine("9. Povratak na glavni izbornik");
            Console.WriteLine("*****************************************");
            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 9!", 1, 9))
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
                case 6:
                    PregledCrneListe();
                    PrikaziIzbornik();
                    break;
                case 7:
                    NaCrnuListu();
                    PrikaziIzbornik();
                    break;
                case 8:
                    BrisiIzCrneListe();
                    PrikaziIzbornik();
                    break;
                case 9:
                    Console.WriteLine("Gotov rad s korisnicima!");
                    break;


            }
        }


        private void PregledKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na listi!");
                PrikaziIzbornik();
            }
            else
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("               Korisnici:                ");
                Console.WriteLine("*****************************************");
                int broj = 1;
                foreach (Korisnik k in Korisnici)
                {
                    Console.WriteLine("\t{0}. {1}", broj++, k);
                }
                Console.WriteLine("*****************************************");
            }
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
            k.IPadresa = ""; // Za sad stavljam prazno, dok ne vidim može li automatski
            Korisnici.Add(k);
        }


        private void PromjenaKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na listi!");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
                var k = Korisnici[index - 1];
                k.Sifra = Ucitavanje.UcitajCijeliBroj("Unesite šifru korisnika (" + k.Sifra + "): ", "Unos mora biti prirodan broj.");
                k.Ime = Ucitavanje.UcitajString("Unesite ime korisnika (" + k.Ime + "): ", "Ime je obavezno.");
                k.Prezime = Ucitavanje.UcitajString("Unesite prezime korisnika (" + k.Prezime + "): ", "Prezime je obavezno.");
                k.Email = Ucitavanje.UcitajString("Unesite e-mail korisnika (" + k.Email + "): ", "E-mail je obavezan.");
                k.Mobitel = Ucitavanje.UcitajString("Unesite broj mobitela korisnika (" + k.Mobitel + "): ", "Broj mobitela je obavezan.");
                k.Grad = Ucitavanje.UcitajString("Unesite grad ili mjesto (" + k.Grad + "): ", "Lokacija je obavezna.");
                // Ostala svojstva može mijenjati samo administrator preko izbornika
                // IPadresa bi trebala ići automatski
            }
        }


        private void BrisanjeKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na listi!");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
                Korisnici.RemoveAt(index - 1);
            }
        }

        private void DodajOvlasti()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na listi!");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
                var k = Korisnici[index - 1];
                k.Uloga = Ucitavanje.UcitajBrojRaspon("Odredite razinu ovlasti: 1 za administratora, 2 za moderatora.", "Treba upisati broj 1 ili 2.", 1, 2);
                k.Lozinka = Ucitavanje.UcitajString("Odredite lozinku za korisnika: ", "Lozinka je obavezna za admina i moderatora.");

            }
        }
        


        private void PregledCrneListe()
        {
            if (KorisniciNaListi.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na crnoj listi!");
                PrikaziIzbornik();
            }
            else
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("*       Korisnici na crnoj listi:       *");
                Console.WriteLine("*****************************************");
                int broj = 1;
                foreach (CrnaLista k in KorisniciNaListi)
                {
                    Console.WriteLine("\t{0}. {1}", broj++, k);
                }
                Console.WriteLine("*****************************************");
            }
        }

        private void NaCrnuListu()
        {
            PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, Korisnici.Count());
            var k = Korisnici[index - 1];
            k.CrnaLista = true;
            var cl = new CrnaLista(k);
            cl.RazlogBlokiranja = Ucitavanje.UcitajString("Upišite razlog blokiranja ovog korisnika: ", "Razlog je obavezan.");
            cl.DatumBlokiranja = Ucitavanje.UcitajDatum("Upišite datum blokiranja: ", "Nije dobar format datuma."); // Za sad stavljam ovako, ali treba ići automatski
            KorisniciNaListi.Add(cl);

        }


        private void BrisiIzCrneListe()
        {
            if (KorisniciNaListi.Count() == 0)
            {
                Console.WriteLine("Nema korisnika na crnoj listi!");
                PrikaziIzbornik();
            }
            else
            {
                PregledCrneListe();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika: ", "Nije dobar odabir.", 1, KorisniciNaListi.Count());
                var k = Korisnici[index - 1];
                k.CrnaLista = true;
                var cl = new CrnaLista(k);
                cl.RazlogBlokiranja = Ucitavanje.UcitajString("Upišite razlog blokiranja ovog korisnika: ", "Razlog je obavezan.");
                cl.DatumBlokiranja = Ucitavanje.UcitajDatum("Upišite datum blokiranja: ", "Nije dobar format datuma."); // Za sad stavljam ovako, ali treba ići automatski
                KorisniciNaListi.Add(cl);
            }
        }


        private void TestniPodaci()
        {

            Korisnici.Add(new Korisnik
            {
                Sifra = 1,
                Uloga = 1,      // administrator
                Lozinka = "Zoki123",
                Ime = "Sanja",
                Prezime = "Habuš",
                Email = "shabus@gmail.com",
                Mobitel = "092 146 3753",
                Grad = "Zaprešić",
                CrnaLista = false,
                IPadresa = "91.152.23.137"

            });


            Korisnici.Add(new Korisnik
            {
                Sifra = 2,
                Uloga = 1,      // administrator
                Lozinka = "Bruno123",
                Ime = "Jasenka",
                Prezime = "Augustinović",
                Email = "jaugustinovic@gmail.com",
                Mobitel = "091 543 6424",
                Grad = "Osijek",
                CrnaLista = false,
                IPadresa = "92.143.54.128"

            });


            Korisnici.Add(new Korisnik
            {
                Sifra = 3,
                Uloga = 2,      // moderator
                Lozinka = "Ivan123",
                Ime = "Ana",
                Prezime = "Marasović",
                Email = "amarasovic@gmail.com",
                Mobitel = "099 234 4422",
                Grad = "Zagreb",
                CrnaLista = false,
                IPadresa = "93.164.72.181"

            });



            Korisnici.Add(new Korisnik
            {
                Sifra = 4,
                Uloga = 2,      // moderator
                Lozinka = "Josip123",
                Ime = "Maja",
                Prezime = "Grgić",
                Email = "mgrgic@gmail.com",
                Mobitel = "095 632 7455",
                Grad = "Sesvete",
                CrnaLista = false,
                IPadresa = "94.164.72.181"

            });



            Korisnici.Add(new Korisnik
            {
                Sifra = 5,
                Uloga = 0,      // obican korisnik
                Lozinka = "",
                Ime = "Ivana",
                Prezime = "Banić",
                Email = "ivana.banic@gmail.com",
                Mobitel = "091 555 7654",
                Grad = "Našice",
                CrnaLista = false,
                IPadresa = "95.131.57.148"

            } );


            Korisnici.Add(new Korisnik
            {
                Sifra = 6,
                Uloga = 0,      // obican korisnik
                Lozinka = "",
                Ime = "Adriana",
                Prezime = "Popović",
                Email = "apopovic@gmail.com",
                Mobitel = "098 323 7532",
                Grad = "Tenja",
                CrnaLista = false,
                IPadresa = "96.163.48.131"

            });


        }

    }
}
