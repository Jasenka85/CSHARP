﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class ObradaOglasa
    {
        int Osifra = 7;      // podesiti kada se uklone testni podaci!

        public List<Oglas> Oglasi { get; }

        private Izbornik Izbornik;

        public ObradaOglasa()
        {
            Oglasi = new List<Oglas>();
           
        }

        public ObradaOglasa(Izbornik izbornik) : this()
        {
            this.Izbornik = izbornik;
            TestniPodaci();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                                        Izbornik za rad s oglasima                                        ");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregledaj postojeće oglase");
            Console.WriteLine("\t 2. Unesi novi oglas");
            Console.WriteLine("\t 3. Promijeni postojeći oglas");
            Console.WriteLine("\t 4. Obriši oglas");
            Console.WriteLine("\t 5. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");
            

            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 5!", 1, 5))
            {
                case 1:
                    PregledOglasa();
                    DetaljiOglasa();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosOglasa();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaOglasa();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeOglasa();
                    PrikaziIzbornik();
                    break;

                case 5:
                    Console.WriteLine("\nGotov rad s oglasima!\n");
                    break;
            }
        }


        public void PregledOglasa()
        {
            if (Oglasi.Count() == 0)
            {
                Console.WriteLine("\nNema oglasa na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("                                                 Oglasi:                                                  ");
                Console.WriteLine("**********************************************************************************************************");
                int broj = 1;

                
                
                foreach (Oglas o in Oglasi)
                {
                   
                        Console.WriteLine("\t{0}. {1}", broj++, o);
                }
               
                Console.WriteLine("**********************************************************************************************************");


            }
        }

        private void DetaljiOglasa()
        {

            while (true)
            {
                if (Ucitavanje.UcitajBool("Želite li detalje o nekom oglasu? Upišite 'da' ili bilo što drugo za ne: ", "Nije dobar unos."))
                {
                    int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojeg želite pregledati: ", "Nije dobar odabir.", 1, Oglasi.Count());
                    var o = Oglasi[index - 1];

                    Console.WriteLine("**********************************************************************************************************");
                    Console.WriteLine("\t Šifra oglasa: {0}", o.Sifra);
                    Console.WriteLine("\t Oglas je: {0}", Ucitavanje.OdrediAktivnost(o.Aktivan));
                    Console.WriteLine("\t Kategorija: {0}", Ucitavanje.OdrediKategoriju(o.Kategorija));
                    Console.WriteLine("\t Datum objave: {0}", o.DatumObjave);
                    Console.WriteLine("\t Naslov: {0}", o.NaslovOglasa);
                    Console.WriteLine("\t Opis: {0}", o.OpisOglasa);
                    Console.WriteLine("\t Vrsta životinje: {0}", o.VrstaZivotinje);

                    if (o.Kategorija == 1)
                    {
                        Console.WriteLine("\t Ime životinje: {0}", o.ImeZivotinje);
                        Console.WriteLine("\t Spol životinje: {0}", o.SpolZivotinje);
                        Console.WriteLine("\t Je li kastrirana: {0}", o.Kastriran);
                        Console.WriteLine("\t Dob životinje: {0}", o.DobZivotinje);
                    }
                    else 
                    {
                        Console.WriteLine("\t Željena pasmina: {0}", o.ImeZivotinje);
                        Console.WriteLine("\t Željeni spol: {0}", o.SpolZivotinje);
                        Console.WriteLine("\t Treba li biti kastrirana: {0}", o.Kastriran);
                        Console.WriteLine("\t Željena dob: {0}", o.DobZivotinje);
                    }


                    Console.WriteLine("\t Ime korisnika: {0}", o.Korisnik.Ime);
                    Console.WriteLine("\t Prezime korisnika: {0}", o.Korisnik.Prezime);
                    Console.WriteLine("\t E-mail: {0}", o.Korisnik.Email);
                    Console.WriteLine("\t Broj mobitela: {0}", o.Korisnik.Mobitel);
                    Console.WriteLine("\t Grad: {0}", o.Korisnik.Grad);
                    //foreach (Fotografija f in Fotografije)
                    //{
                    //    Console.WriteLine("\t{0}. {1}", broj++, f);
                    //}
                    
                    
                }
                else
                {
                    break;
                }
            }

        }


        private void UnosOglasa()
        {
            var o = new Oglas();
            Izbornik.ObradaKorisnika.PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika koji objavljuje oglas, ili upišite 0 za unos novog korisnika: ", "Nije dobar odabir.", 0, Izbornik.ObradaKorisnika.Korisnici.Count());
            if (index == 0)
            {
                Izbornik.ObradaKorisnika.UnosKorisnika();
                o.Korisnik = Izbornik.ObradaKorisnika.Korisnici[^1];
            }
            else
            {
                o.Korisnik = Izbornik.ObradaKorisnika.Korisnici[index - 1];
            }

            o.Sifra = Osifra++;
            o.Aktivan = false;      //Oglas se aktivira kada ga odobri administrator
            o.Kategorija = Ucitavanje.UcitajBrojRaspon("Odaberite kategoriju: 1 za poklanjam životinju ili 2 za želim udomiti životinju: ", "Treba upisati broj 1 ili 2.", 1, 2);
            o.DatumObjave = DateTime.Now;
            o.NaslovOglasa= Ucitavanje.UcitajString("Unesite naslov oglasa: ", "Naslov je obavezan.");
            o.OpisOglasa = Ucitavanje.UcitajString("Unesite opis oglasa: ", "Opis je obavezan.");

            if (o.Kategorija == 1)  //Pitanja su malo drugačija ako korisnik poklanja životinju...
            {
                o.VrstaZivotinje = Ucitavanje.UcitajString("Unesite vrstu životinje: ", "Vrsta je obavezna.");
                o.ImeZivotinje = Ucitavanje.UcitajString("Unesite ime životinje: ", "Ime je obavezno.");
                o.SpolZivotinje = Ucitavanje.UcitajString("Unesite spol životinje: ", "Spol je obavezan.");
                o.Kastriran = Ucitavanje.UcitajString("Je li kastrirana? ", "Odgovor je obavezan.");
                o.DobZivotinje = Ucitavanje.UcitajString("Unesite dob životinje: ", "Dob je obavezna.");
            }
            else    //... ili ako želi udomiti životinju. Ali koriste ista svojstva.
            {
                o.VrstaZivotinje = Ucitavanje.UcitajString("Koju vrstu životinje želite udomiti? ", "Vrsta je obavezna.");
                o.ImeZivotinje = Ucitavanje.UcitajString("Treba li biti određene pasmine ili rasta? ", "Odgovor je obavezan.");
                o.SpolZivotinje = Ucitavanje.UcitajString("Kojeg spola treba biti? ", "Odgovor je obavezan.");
                o.Kastriran = Ucitavanje.UcitajString("Treba li biti kastrirana/sterilizirana? ", "Odgovor je obavezan.");
                o.DobZivotinje = Ucitavanje.UcitajString("Je li vam važna dob životinje? Ako da, koju preferirate? ", "Odgovor je obavezan.");

            }
            Oglasi.Add(o);
            Console.WriteLine("\nOglas je uspješno dodan na popis.\n");
        }

        

        private void PromjenaOglasa()
        {
            if (Oglasi.Count() == 0)
            {
                Console.WriteLine("\nNema oglasa na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledOglasa();    //Prvo bira oglas kojeg želi mijenjati... ili može odustati
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojeg želite promijeniti (ili 0 za izlaz): ", "Nije dobar odabir.", 0, Oglasi.Count());
                
                if (index == 0)
                {
                    Console.WriteLine("\nOglas nije promijenjen.\n");
                    PrikaziIzbornik();
                }
                else
                {

                    var o = Oglasi[index - 1];

                    Izbornik.ObradaKorisnika.PregledKorisnika();   // Može promijeniti korisnika koji je objavio oglas (a ako izabere istog, ažurirat će se njegovi podaci ako je došlo do neke promjene)
                    int index2 = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika koji objavljuje oglas (" + o.Korisnik + "): ", "Nije dobar odabir.", 1, Izbornik.ObradaKorisnika.Korisnici.Count());
                    o.Korisnik = Izbornik.ObradaKorisnika.Korisnici[index2 - 1];

                    // šifra i datum objave idu automatski i ne mogu se mijenjati
                    o.Aktivan = Ucitavanje.UcitajBool("Je li oglas aktivan? Upišite 'da' ili bilo što drugo za ne: ", "Nije dobar unos.");
                    o.Kategorija = Ucitavanje.UcitajBrojRaspon("Odaberite kategoriju: 1 za poklanjam životinju ili 2 za želim udomiti životinju (" + o.Kategorija + "): ", "Treba upisati broj 1 ili 2.", 1, 2);

                    Console.WriteLine("\nU nastavku unesite promjene ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");

                    o.NaslovOglasa = Ucitavanje.UcitajPromjenu("Unesite naslov oglasa (" + o.NaslovOglasa + "): ", o.NaslovOglasa);
                    o.OpisOglasa = Ucitavanje.UcitajPromjenu("Unesite opis oglasa (" + o.OpisOglasa + "): ", o.OpisOglasa);

                    if (o.Kategorija == 1)  //Pitanja su malo drugačija ako korisnik poklanja životinju...
                    {
                        o.VrstaZivotinje = Ucitavanje.UcitajPromjenu("Unesite vrstu životinje (" + o.VrstaZivotinje + "): ", o.VrstaZivotinje);
                        o.ImeZivotinje = Ucitavanje.UcitajPromjenu("Unesite ime životinje (" + o.ImeZivotinje + "): ", o.ImeZivotinje);
                        o.SpolZivotinje = Ucitavanje.UcitajPromjenu("Unesite spol životinje (" + o.SpolZivotinje + "): ", o.SpolZivotinje);
                        o.Kastriran = Ucitavanje.UcitajPromjenu("Je li kastrirana? (" + o.Kastriran + "): ", o.Kastriran);
                        o.DobZivotinje = Ucitavanje.UcitajPromjenu("Unesite dob životinje (" + o.DobZivotinje + "): ", o.DobZivotinje);
                    }
                    else    //... ili ako želi udomiti životinju. Ali koriste ista svojstva.
                    {
                        o.VrstaZivotinje = Ucitavanje.UcitajPromjenu("Koju vrstu životinje želite udomiti? (" + o.VrstaZivotinje + "): ", o.VrstaZivotinje);
                        o.ImeZivotinje = Ucitavanje.UcitajPromjenu("Treba li biti određene pasmine ili rasta? (" + o.ImeZivotinje + "): ", o.ImeZivotinje);
                        o.SpolZivotinje = Ucitavanje.UcitajPromjenu("Kojeg spola treba biti? (" + o.SpolZivotinje + "): ", o.SpolZivotinje);
                        o.Kastriran = Ucitavanje.UcitajPromjenu("Treba li biti kastrirana/sterilizirana? (" + o.Kastriran + "): ", o.Kastriran);
                        o.DobZivotinje = Ucitavanje.UcitajPromjenu("Je li vam važna dob životinje? Ako da, koju preferirate? (" + o.DobZivotinje + "): ", o.DobZivotinje);

                    }


                    Console.WriteLine("\nOglas je uspješno promijenjen.\n");
                }
                
            }
        }

        private void BrisanjeOglasa()
        {
            if (Oglasi.Count() == 0)
            {
                Console.WriteLine("\nNema oglasa na listi!\n");
                PrikaziIzbornik();
            }
            else
            {
                PregledOglasa();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojeg želite obrisati (ili 0 za izlaz): ", "Nije dobar odabir.", 0, Oglasi.Count());
                if (index == 0)
                {
                    Console.WriteLine("\nOglas nije obrisan.\n");
                    
                }
                else
                {

                    Oglasi.RemoveAt(index - 1);
                    Console.WriteLine("\nOglas je uspješno obrisan s popisa.\n");
                }
            }
        }
        

        private void TestniPodaci()
        {
            
            var o1 = new Oglas();
            o1.Sifra = 1;
            o1.Aktivan = true;
            o1.Korisnik = Izbornik.ObradaKorisnika.Korisnici[2];
            o1.Kategorija = 1;
            o1.DatumObjave = DateTime.Parse("20.03.2023");
            o1.NaslovOglasa = "Fifi traži dom!";
            o1.OpisOglasa = "Bivši vlasnici su ju napustili zbog odlaska u inozemstvo. Traži dobrog udomitelja.";
            o1.VrstaZivotinje = "Kunić";
            o1.ImeZivotinje = "Fifi";
            o1.SpolZivotinje = "Ženka";
            o1.Kastriran = "Nije";
            o1.DobZivotinje = "Oko 6 mjeseci";
            Oglasi.Add(o1);

            
            Oglasi.Add(new Oglas
            {
                Sifra = 2,
                Aktivan = true,
                Korisnik = Izbornik.ObradaKorisnika.Korisnici[0],
                Kategorija = 1,
                DatumObjave = DateTime.Parse("25.04.2023"),
                NaslovOglasa = "Bubi traži novi dom!",
                OpisOglasa = "Bivši vlasnik ga je predao udruzi jer se djeca više ne žele brinuti za njega.",
                VrstaZivotinje = "Zamorčić",
                ImeZivotinje = "Bubi",
                SpolZivotinje = "Mužjak",
                Kastriran = "Nije",
                DobZivotinje = "1 godinu"
                
            });


            Oglasi.Add(new Oglas
            {
                Sifra = 3,
                Aktivan = true,
                Korisnik = Izbornik.ObradaKorisnika.Korisnici[1],
                Kategorija = 1,
                DatumObjave = DateTime.Parse("08.05.2023"),
                NaslovOglasa = "Mambo traži dom!",
                OpisOglasa = "Mladi kunić nađen na parkingu u Osijeku, traži dobrog udomitelja.",
                VrstaZivotinje = "Kunić",
                ImeZivotinje = "Mambo",
                SpolZivotinje = "Mužjak",
                Kastriran = "Da",
                DobZivotinje = "Oko 5 mjeseci"
                
            });


            Oglasi.Add(new Oglas
            {
                Sifra = 4,
                Aktivan = true,
                Korisnik = Izbornik.ObradaKorisnika.Korisnici[5],
                Kategorija = 2,
                DatumObjave = DateTime.Parse("08.05.2023"),
                NaslovOglasa = "Sheldon traži društvo!",
                OpisOglasa = "Tražimo mužjaka kako Miki više ne bi bio sam. Ima 1 godinu.",
                VrstaZivotinje = "Zamorčić",
                ImeZivotinje = "dugodlaki",
                SpolZivotinje = "Mužjak",
                Kastriran = "Nije važno",
                DobZivotinje = "Do 1 godinu"
                
            });


            Oglasi.Add(new Oglas
            {
                Sifra = 5,
                Aktivan = true,
                Korisnik = Izbornik.ObradaKorisnika.Korisnici[4],
                Kategorija = 2,
                DatumObjave = DateTime.Parse("08.05.2023"),
                NaslovOglasa = "Tražim činčilu!",
                OpisOglasa = "Želim udomiti činčilu, po mogućnosti mladog mužjaka.",
                VrstaZivotinje = "Činčila",
                ImeZivotinje = "dugorepa",
                SpolZivotinje = "Nije važno",
                Kastriran = "Nije važno",
                DobZivotinje = "Do 1 godinu"
            });


            Oglasi.Add(new Oglas
            {
                Sifra = 6,
                Aktivan = true,
                Korisnik = Izbornik.ObradaKorisnika.Korisnici[5],
                Kategorija = 2,
                DatumObjave = DateTime.Parse("08.05.2023"),
                NaslovOglasa = "Želim udomiti hrčka",
                OpisOglasa = "Želim udomiti bebu hrčka, po mogućnosti sirijskog, ali može i ruski ili roborovski.",
                VrstaZivotinje = "Hrčak",
                ImeZivotinje = "sirijski",
                SpolZivotinje = "Nije važno",
                Kastriran = "Ne",
                DobZivotinje = "Do 3 mjeseca"
            });
            

        }
        
        

    }
}
