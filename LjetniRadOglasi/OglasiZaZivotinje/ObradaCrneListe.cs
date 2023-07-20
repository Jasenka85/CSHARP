using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class ObradaCrneListe
    {
        public List<CrnaLista> KorisniciNaListi { get; }
        private Izbornik Izbornik;

        public ObradaCrneListe()
        {
            KorisniciNaListi = new List<CrnaLista>();
        }

        public ObradaCrneListe(Izbornik izbornik) : this()
        {
            this.Izbornik = izbornik;
        }


        public void PrikaziIzbornik()
        {
            Console.WriteLine("*******************************************************");
            Console.WriteLine("                Izbornik za crnu listu:                ");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("1. Pregledaj crnu listu");
            Console.WriteLine("2. Stavi korisnika na crnu listu");
            Console.WriteLine("3. Obriši korisnika iz crne liste");
            Console.WriteLine("4. Povratak na glavni izbornik");
            Console.WriteLine("*******************************************************");
            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 4!", 1, 4))
            {
   
                case 1:
                    PregledCrneListe();
                    PrikaziIzbornik();
                    break;
                case 2:
                    NaCrnuListu();
                    PrikaziIzbornik();
                    break;
                case 3:
                    BrisiIzCrneListe();
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("\nGotov rad s crnom listom!\n");
                    break;


            }
        }







        private void PregledCrneListe()
        {
            if (KorisniciNaListi.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na crnoj listi!\n");
            }
            else
            {
                Console.WriteLine("*******************************************************");
                Console.WriteLine("*              Korisnici na crnoj listi:              *");
                Console.WriteLine("*******************************************************");
                int broj = 1;
                foreach (CrnaLista k in KorisniciNaListi)
                {
                    Console.WriteLine("\t{0}. {1}", broj++, k);
                }
                
            }
        }

        private void NaCrnuListu()
        {
            var cl = new CrnaLista();
            Izbornik.ObradaKorisnika.PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite staviti na crnu listu: ", "Nije dobar odabir.", 1, Izbornik.ObradaKorisnika.Korisnici.Count());
            cl.Korisnik = Izbornik.ObradaKorisnika.Korisnici[index - 1];
            cl.RazlogBlokiranja = Ucitavanje.UcitajString("Upišite razlog blokiranja ovog korisnika: ", "Razlog je obavezan.");
            cl.DatumBlokiranja = Ucitavanje.UcitajDatum("Upišite datum blokiranja: ", "Nije dobar format datuma."); // Za sad stavljam ovako, ali treba ići automatski
            KorisniciNaListi.Add(cl);
            Console.WriteLine("\nKorisnik je uspješno dodan na crnu listu.\n");

        }


        private void BrisiIzCrneListe()
        {
            if (KorisniciNaListi.Count() == 0)
            {
                Console.WriteLine("\nNema korisnika na crnoj listi!\n");
            }
            else
            {
                PregledCrneListe();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite obrisati iz crne liste: ", "Nije dobar odabir.", 1, KorisniciNaListi.Count());
                KorisniciNaListi.RemoveAt(index-1);
                Console.WriteLine("\nKorisnik je uspješno obrisan iz crne liste.\n");
            }
        }



    }
}
