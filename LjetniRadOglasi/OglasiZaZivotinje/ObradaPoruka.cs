using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class ObradaPoruka
    {

        int Psifra = 6;

        

        private Izbornik Izbornik;

        public ObradaPoruka()
        {
          // ne pravim novu listu poruka, jer svaki oglas ima svoju listu - samo ju dohvatim

        }

        public ObradaPoruka(Izbornik izbornik) : this()
        {
            this.Izbornik = izbornik;
          
        }



        public void PrikaziIzbornik()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                                        Izbornik za rad s porukama                                        ");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregledaj poruke");
            Console.WriteLine("\t 2. Pošalji poruku");
            Console.WriteLine("\t 3. Obriši poruku");       //poruke se šalju na e-mail korisnika i ne mogu se mijenjati, samo obrisati
            Console.WriteLine("\t 4. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");


            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 4!", 1, 4))
            {
                case 1:
                    PregledajPoruke();
                    PrikaziIzbornik();
                    break;
                case 2:
                    PosaljiPoruku();
                    PrikaziIzbornik();
                    break;
                case 3:
                    ObrisiPoruku();
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("\nGotov rad s porukama!\n");
                    break;
            }
        }


       





        private int PregledajPoruke()
        {

            Izbornik.ObradaOglasa.PregledOglasa();

            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa za kojeg želite vidjeti poruke (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Izbornik.ObradaOglasa.Oglasi.Count());
            if (index == 0)
            {
                return 0;
            }
            
            else
            {
                var o = Izbornik.ObradaOglasa.Oglasi[index - 1];
                if (o.Poruke.Count() == 0)
                {
                    Console.WriteLine("\nNema poruka za ovaj oglas.\n");
                    return 0;
                }
                else
                {
                    Console.WriteLine("**********************************************************************************************************");
                    int broj = 1;
                    foreach (Poruka p in o.Poruke)
                    {

                            Console.WriteLine("\n\t\t{0}. poruka:", broj++);
                            Console.WriteLine("\t\tIme pošiljatelja: {0}", p.ImePosiljatelja);
                            Console.WriteLine("\t\tE-mail pošiljatelja: {0}", p.EmailPosiljatelja);
                            Console.WriteLine("\t\tPoruka: {0}", p.TekstPoruke);
                            Console.WriteLine("\t\tDatum poruke: {0}", p.DatumPoruke);
                    }
                    Console.WriteLine("**********************************************************************************************************");

                    return index;
                }
                
            }
        }








        private void PosaljiPoruku()
        {
            Izbornik.ObradaOglasa.PregledOglasa();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojem želite poslati poruku (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Izbornik.ObradaOglasa.Oglasi.Count());
            if (index != 0)
            {
                var o = Izbornik.ObradaOglasa.Oglasi[index - 1];
                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("\t Kategorija: {0}", Ucitavanje.OdrediKategoriju(o.Kategorija));
                Console.WriteLine("\t Naslov: {0}", o.NaslovOglasa);
                Console.WriteLine("\t Opis: {0}", o.OpisOglasa);
                Console.WriteLine("\t Vrsta životinje: {0}", o.VrstaZivotinje);
                Console.WriteLine("\t Ime korisnika: {0}", o.Korisnik.Ime);
                Console.WriteLine("\t Prezime korisnika: {0}", o.Korisnik.Prezime);
                Console.WriteLine("\t E-mail: {0}", o.Korisnik.Email);
                Console.WriteLine("\t Broj mobitela: {0}", o.Korisnik.Mobitel);
                Console.WriteLine("\t Grad: {0}", o.Korisnik.Grad);
                Console.WriteLine("**********************************************************************************************************");

                var p = new Poruka();    
                p.Sifra = Psifra++;
                p.ImePosiljatelja = Ucitavanje.UcitajString("Unesite svoje ime: ", "Ime je obavezno.");
                p.EmailPosiljatelja = Ucitavanje.UcitajString("Unesite svoj e-mail, da vam korisnik moze odgovoriti: ", "E-mail je obavezan.");
                p.TekstPoruke = Ucitavanje.UcitajString("Unesite poruku: ", "Poruka je obavezna.");
                p.DatumPoruke = DateTime.Now;
                Console.WriteLine("\nPoruka je uspješno poslana!\n");
                o.Poruke.Add(p);
            }
        }

        private void ObrisiPoruku()
        {
            int index = PregledajPoruke();
            if (index!=0)
            {
            var o = Izbornik.ObradaOglasa.Oglasi[index - 1];
            int index2 = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj poruke koju želite obrisati (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, o.Poruke.Count());
                if (index2 != 0)
                {
                    o.Poruke.RemoveAt(index2 - 1);
                    Console.WriteLine("\nPoruka je uspješno obrisana.\n");
                }
            }
        }

    }
}
