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
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                                         Izbornik za crnu listu:                                          ");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregledaj crnu listu");
            Console.WriteLine("\t 2. Stavi korisnika na crnu listu");
            Console.WriteLine("\t 3. Obriši korisnika iz crne liste");
            Console.WriteLine("\t 4. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");
          
            switch (Ucitavanje.UcitajBrojRaspon("Odaberite redni broj stavke iz izbornika: ", "Odabir mora biti broj između 1 i 4!", 1, 4))
            {
   
                case 1:
                    PregledCrneListe();
                    PrikaziDetalje();
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
                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("*                                        Korisnici na crnoj listi:                                       *");
                Console.WriteLine("**********************************************************************************************************");
                int broj = 1;
                foreach (CrnaLista k in KorisniciNaListi)
                {
                    Console.WriteLine("\t{0}. {1}", broj++, k);
                }
                Console.WriteLine("**********************************************************************************************************");
            }
        }


        private void PrikaziDetalje()
        {

            while (true)
            {
                if (Ucitavanje.UcitajBool("Želite li detalje o nekom korisniku? \nUpišite 'da' ili bilo što drugo za ne: "))
                {
                    int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite pregledati: ", "Nije dobar odabir.", 1, KorisniciNaListi.Count());
                    var k = KorisniciNaListi[index - 1];
                    Console.WriteLine("**********************************************************************************************************");
                    Console.WriteLine("\t Sifra: {0}", k.Korisnik.Sifra);
                    Console.WriteLine("\t Ime: {0}", k.Korisnik.Ime);
                    Console.WriteLine("\t Prezime: {0}", k.Korisnik.Prezime);
                    Console.WriteLine("\t Uloga: {0}", Ucitavanje.OdrediUlogu(k.Korisnik.Uloga));
                    Console.WriteLine("\t E-mail: {0}", k.Korisnik.Email);
                    Console.WriteLine("\t Broj mobitela: {0}", k.Korisnik.Mobitel);
                    Console.WriteLine("\t Grad: {0}", k.Korisnik.Grad);
                    Console.WriteLine("\t IP adresa: {0}", k.Korisnik.IPadresa);
                    Console.WriteLine("\t Razlog blokiranja: {0}", k.RazlogBlokiranja);
                    Console.WriteLine("\t Datum blokiranja: {0}", k.DatumBlokiranja);
                    Console.WriteLine("**********************************************************************************************************");
                }
                else
                {
                    break;
                }
            }

        }



        private void NaCrnuListu()
        {
            int zastavica = 0;
            var cl = new CrnaLista();
            Izbornik.ObradaKorisnika.PregledKorisnika();
            int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika kojeg želite staviti na crnu listu: ", "Nije dobar odabir.", 1, Izbornik.ObradaKorisnika.Korisnici.Count());
            cl.Korisnik = Izbornik.ObradaKorisnika.Korisnici[index - 1];
            for (int i = 0; i< KorisniciNaListi.Count(); i++)
            {
                if (cl.Korisnik.Sifra == KorisniciNaListi[i].Korisnik.Sifra)
                {
                    Console.WriteLine("\nOvaj korisnik je već na crnoj listi!\n");
                    zastavica++;
                    break;
                }
            }
            if (zastavica == 0)
            {
                cl.RazlogBlokiranja = Ucitavanje.UcitajString("Upišite razlog blokiranja ovog korisnika: ", "Razlog je obavezan.");
                cl.DatumBlokiranja = DateTime.Now; 
                KorisniciNaListi.Add(cl);
                Console.WriteLine("\nKorisnik je uspješno dodan na crnu listu.\n");
            }
         

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
