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
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                      Izbornik za rad sa smjerovima                                     *");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Pregled postojećih smjerova");
            Console.WriteLine("\t 2. Unos novog smjera");
            Console.WriteLine("\t 3. Promjena postojećeg smjera");
            Console.WriteLine("\t 4. Brisanje smjera");
            Console.WriteLine("\t 5. Povratak na glavni izbornik");
            Console.WriteLine("**********************************************************************************************************");

            switch (Pomocno.ucitajBrojRaspon("\nOdaberite stavku izbornika smjera: ", "Odabir mora biti 1-5", 1, 5))
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
                    BrisanjeSmjera();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\nGotov rad sa smjerovima.\n");
                    break;
            }

        }

        

        public void PrikaziSmjerove()
        {
            if (Smjerovi.Count() == 0)
            {
                Console.WriteLine("\nNema smjerova na listi!\n");
               
            }
            else
            {
                Console.WriteLine("**********************************************************************************************************");
                Console.WriteLine("*                                               Smjerovi:                                                *");
                Console.WriteLine("**********************************************************************************************************");
                int b = 1;
                foreach (Smjer smjer in Smjerovi)
                {
                    Console.WriteLine("\t{0}. {1}", b++, smjer.Naziv);
                }
                Console.WriteLine("**********************************************************************************************************");
            } 
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
            Console.WriteLine("\nSmjer je uspješno dodan na popis.\n");

        }

       

        private void PromjenaSmjera()
        {
            if (Smjerovi.Count() == 0)
            {
                Console.WriteLine("\nNema smjerova na listi!\n");

            }
            else
            {
                PrikaziSmjerove();

                int index = Pomocno.ucitajBrojRaspon("Odaberite redni broj smjera kojeg želite promijeniti (ili 0 za izlaz): ", "Nije dobar odabir!", 0, Smjerovi.Count());

                if (index != 0)
                {
                    var s = Smjerovi[index - 1];
                    Console.WriteLine("\nU nastavku unesite nove podatke ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");
                    s.Sifra = Pomocno.UcitajPromjenuBroja("Unesite novu šifru smjera ili enter (" + s.Sifra + "): ", s.Sifra);
                    s.Naziv = Pomocno.UcitajPromjenuStringa("Unesite novi naziv smjera ili enter (" + s.Naziv + "): ", s.Naziv);
                    s.Trajanje = Pomocno.UcitajPromjenuBroja("Unesite novo trajanje smjera u satima ili enter (" + s.Trajanje + "): ", s.Trajanje);
                    s.Cijena = Pomocno.UcitajPromjenuDecBroja("Unesite novu cijenu ili enter, stavite . za decimalni dio (" + s.Cijena + "): ", s.Cijena);
                    s.Upisnina = Pomocno.UcitajPromjenuDecBroja("Unesite novu upisninu ili enter, stavite . za decimalni dio (" + s.Upisnina + "): ", s.Upisnina);
                    s.Verificiran = Pomocno.ucitajBool("Smjer verificiran? Unesite 'da' ili bilo što drugo za ne (" + (s.Verificiran ? "da" : "ne") + "): ");
                    Console.WriteLine("\nSmjer je uspješno promijenjen.\n");
                }
            }
        }


        private void BrisanjeSmjera()
        {
            if (Smjerovi.Count == 0)
            {
                Console.WriteLine("\nNema smjerova na listi!\n");
            }
            else
            {
                PrikaziSmjerove();
                int index = Pomocno.ucitajBrojRaspon("Odaberi redni broj smjera za brisanje: ", "Nije dobar odabir!", 1, Smjerovi.Count());
                Smjerovi.RemoveAt(index - 1);
                Console.WriteLine("\nSmjer je uspješno obrisan.\n");
            }
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
