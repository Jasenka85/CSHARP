using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class ObradaKorisnika
    {
        int Ksifra = 7;      // podesiti kada se uklone testni podaci!
        public List<Korisnik> Korisnici { get; }

        
        
        public ObradaKorisnika()
        { 
            Korisnici = new List<Korisnik>();
            TestniPodaci();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                                      Izbornik za rad s korisnicima                                       ");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregledaj postojeće korisnike");
            Console.WriteLine("\t 2. Unesi novog korisnika");
            Console.WriteLine("\t 3. Promijeni postojećeg korisnika");
            Console.WriteLine("\t 4. Obriši korisnika");
            Console.WriteLine("\t 5. Dodaj admina ili moderatora");
            Console.WriteLine("\t 6. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 6!", 1, 6))
            { 
            case 1:
                    PregledKorisnika();
                    PrikaziDetalje();
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
                    Console.WriteLine("\nGotov rad s korisnicima!\n");
                    break;
            }
        }


        public void PregledKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("                                               Korisnici:                                                 ");
                Console.WriteLine("**********************************************************************************************************");
                int broj = 1;
                foreach (Korisnik k in Korisnici)
                {
                    Console.WriteLine("\t{0}. {1}", broj++, k);
                }
                Console.WriteLine("**********************************************************************************************************");
                
                
                
            }
        }


        private void PrikaziDetalje()
        {
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite pregledati (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Korisnici.Count());
            if (index != 0)
            {
                var k = Korisnici[index - 1];

                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("\t Sifra: {0}", k.Sifra);
                Console.WriteLine("\t Ime: {0}", k.Ime);
                Console.WriteLine("\t Prezime: {0}", k.Prezime);
                Console.WriteLine("\t Uloga: {0}", Ucitavanje.OdrediUlogu(k.Uloga));
                Console.WriteLine("\t E-mail: {0}", k.Email);
                Console.WriteLine("\t Broj mobitela: {0}", k.Mobitel);
                Console.WriteLine("\t Grad: {0}", k.Grad);
            }
            
            
        }

        public void UnosKorisnika()
        {
            var k = new Korisnik();
            k.Sifra = Ksifra++;
            k.Uloga = 0;      // Po defaultu je 0 = obican korisnik, samo admin može naknadno dodijeliti: 1 = admin, 2 = moderator
            k.Lozinka = "";     // Lozinku imaju samo admin i moderator, to upisuje admin kada im dodjeljuje ovlasti
            k.Ime = Ucitavanje.UcitajString("Unesite ime korisnika: ", "Ime je obavezno.");
            k.Prezime = Ucitavanje.UcitajString("Unesite prezime korisnika: ", "Prezime je obavezno.");
            k.Email = Ucitavanje.UcitajString("Unesite e-mail korisnika: ", "E-mail je obavezan.");
            k.Mobitel = Ucitavanje.UcitajString("Unesite broj mobitela korisnika: ", "Broj mobitela je obavezan.");
            k.Grad = Ucitavanje.UcitajString("Unesite grad ili mjesto: ", "Lokacija je obavezna.");
            k.IPadresa = ""; // Za sad stavljam prazno, dok ne vidim može li automatski
            Korisnici.Add(k);
            Console.WriteLine("\nKorisnik je uspješno dodan na popis.\n");
        }


        private void PromjenaKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite promijeniti (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Korisnici.Count());
                if (index != 0)
                {
                    var k = Korisnici[index - 1];
                    Console.WriteLine("\nUnesite promjene ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");

                    k.Ime = Ucitavanje.UcitajPromjenu("Unesite novo ime korisnika ili enter (" + k.Ime + "): ", k.Ime);
                    k.Prezime = Ucitavanje.UcitajPromjenu("Unesite novo prezime korisnika ili enter (" + k.Prezime + "): ", k.Prezime);
                    k.Email = Ucitavanje.UcitajPromjenu("Unesite novi e-mail korisnika ili enter (" + k.Email + "): ", k.Email);
                    k.Mobitel = Ucitavanje.UcitajPromjenu("Unesite novi broj mobitela ili enter (" + k.Mobitel + "): ", k.Mobitel);
                    k.Grad = Ucitavanje.UcitajPromjenu("Unesite novi grad ili enter (" + k.Grad + "): ", k.Grad);

                    Console.WriteLine("\nKorisnik je uspješno promijenjen.\n");
                    // Sifra ide automatski, ne moze se mijenjati
                    // Ostala svojstva može mijenjati samo administrator preko izbornika
                    // IPadresa bi trebala ići automatski
                }
            }
        }


        private void BrisanjeKorisnika()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite obrisati (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Korisnici.Count());
                if (index != 0)
                {
                    var k = Korisnici[index - 1];
                    if (k.Uloga == 1 || k.Uloga == 2)
                    {
                        Console.WriteLine("\nNe mogu obrisati administratora ili moderatora.\n");
                    }
                    else
                    {
                        Korisnici.RemoveAt(index - 1);
                        Console.WriteLine("\nKorisnik je uspješno obrisan s popisa.\n");
                    }
                }
            }
        }

        private void DodajOvlasti()
        {
            if (Korisnici.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledKorisnika();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojem želite promijeniti ulogu (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Korisnici.Count());
                if (index != 0)
                {
                    var k = Korisnici[index - 1];
                    k.Uloga = Ucitavanje.UcitajBrojRaspon("Odredite razinu ovlasti: 0 za korisnika, 1 za administratora, 2 za moderatora: ", "Treba upisati broj 0, 1 ili 2.", 0, 2);
                    if (k.Lozinka == "" && k.Uloga != 0)
                    {
                        k.Lozinka = Ucitavanje.UcitajString("Odredite lozinku za korisnika: ", "Lozinka je obavezna za admina i moderatora.");
                    }
                    Console.WriteLine("\nKorisniku je uspješno promijenjena uloga\n");
                }
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
                IPadresa = "96.163.48.131"

            });




        }

    }
}
