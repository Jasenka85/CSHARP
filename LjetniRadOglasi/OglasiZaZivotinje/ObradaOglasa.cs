using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace OglasiZaZivotinje
{
    internal class ObradaOglasa
    {
        int Osifra = 7;      // podesiti kada se uklone testni podaci!
        int Fsifra = 4;
       
        public List<Oglas> Oglasi { get; }
        public List<Poruka> Poruke { get; }

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
                    if (Oglasi.Count() != 0)
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
            o.NaslovOglasa = Ucitavanje.UcitajString("Unesite naslov oglasa: ", "Naslov je obavezan.");
            o.OpisOglasa = Ucitavanje.UcitajString("Unesite opis oglasa: ", "Opis je obavezan.");

            if (o.Kategorija == 1)  //Ako korisnik poklanja životinju, u oglas se mogu dodati fotografije
            {
                o.VrstaZivotinje = Ucitavanje.UcitajString("Unesite vrstu životinje: ", "Vrsta je obavezna.");
                o.ImeZivotinje = Ucitavanje.UcitajString("Unesite ime životinje: ", "Ime je obavezno.");
                o.SpolZivotinje = Ucitavanje.UcitajString("Unesite spol životinje: ", "Spol je obavezan.");
                o.Kastriran = Ucitavanje.UcitajString("Je li kastrirana? ", "Odgovor je obavezan.");
                o.DobZivotinje = Ucitavanje.UcitajString("Unesite dob životinje: ", "Dob je obavezna.");
                o.Fotografije = new List<Fotografija>();
                while (true)
                {
                    if (Ucitavanje.UcitajBool("Želite li dodati fotografiju životinje? Upišite 'da' ili bilo što drugo za ne: "))
                    {
                        var f = new Fotografija();
                        f.Sifra = Fsifra++;
                        f.Naziv = Ucitavanje.UcitajString("Unesite naziv fotografije: ", "Naziv je obavezan.");
                        f.Link = Ucitavanje.UcitajString("Unesite link fotografije: ", "Link je obavezan.");
                        o.Fotografije.Add(f);

                    }
                    else
                    {
                        break;
                    }
                }

            }


            else    //... ako korisnik želi udomiti životinju, pitanja su malo drugačija i nema fotografija
            {
                o.VrstaZivotinje = Ucitavanje.UcitajString("Koju vrstu životinje želite udomiti? ", "Vrsta je obavezna.");
                o.ImeZivotinje = Ucitavanje.UcitajString("Treba li biti određene pasmine ili rasta? ", "Odgovor je obavezan.");
                o.SpolZivotinje = Ucitavanje.UcitajString("Kojeg spola treba biti? ", "Odgovor je obavezan.");
                o.Kastriran = Ucitavanje.UcitajString("Treba li biti kastrirana/sterilizirana? ", "Odgovor je obavezan.");
                o.DobZivotinje = Ucitavanje.UcitajString("Je li vam važna dob životinje? Ako da, koju preferirate? ", "Odgovor je obavezan.");

            }

            o.Poruke = new List<Poruka>();  //svaki oglas ima otvorenu listu za poruke, u koju će se upisivati poruke od drugih korisnika 
                                            //ideja je da se te poruke šalju direktno na e-mail korisnika, a kopija ostaje vidljiva administratoru
            Oglasi.Add(o);
            Console.WriteLine("\nOglas je uspješno dodan na popis.\n");
        }






        public void DetaljiOglasa()
        {
      
                int index = Ucitavanje.UcitajBrojRaspon("Za detalje odaberite redni broj oglasa (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Oglasi.Count());
                if (index != 0)
                { 
                    var o = Oglasi[index - 1];

                    Console.WriteLine("**********************************************************************************************************");
                    Console.WriteLine("\t Šifra oglasa: {0}", o.Sifra);
                    Console.WriteLine("\t Oglas je: {0}", Ucitavanje.OdrediAktivnost(o.Aktivan));
                    Console.WriteLine("\t Kategorija: {0}", Ucitavanje.OdrediKategoriju(o.Kategorija));
                    Console.WriteLine("\t Datum objave: {0}", o.DatumObjave);
                    Console.WriteLine("\t Naslov: {0}", o.NaslovOglasa);
                    Console.WriteLine("\t Opis: {0}", o.OpisOglasa);
                    Console.WriteLine("\t Vrsta životinje: {0}", o.VrstaZivotinje);

                    if (o.Kategorija == 1)  //Oglasi u kojima se poklanja životinja mogu imati fotografije
                    {
                        Console.WriteLine("\t Ime životinje: {0}", o.ImeZivotinje);
                        Console.WriteLine("\t Spol životinje: {0}", o.SpolZivotinje);
                        Console.WriteLine("\t Je li kastrirana: {0}", o.Kastriran);
                        Console.WriteLine("\t Dob životinje: {0}", o.DobZivotinje);
                        if (o.Fotografije.Count() == 0)
                        {
                            Console.WriteLine("\n\tU oglasu nema fotografija.\n");
                        }
                        else
                        {
                            Console.WriteLine("\t Fotografije:");
                            int broj = 1;
                            foreach (Fotografija f in o.Fotografije)
                            {
                                Console.WriteLine("\t\t {0}. {1}", broj++, f);
                            }
                        }
                    }
                    else        //Oglasi u kojima se traži životinja nemaju fotografije i nazivi su malo drugačiji
                    {
                        Console.WriteLine("\t Željena pasmina: {0}", o.ImeZivotinje);
                        Console.WriteLine("\t Željeni spol: {0}", o.SpolZivotinje);
                        Console.WriteLine("\t Treba li biti kastrirana: {0}", o.Kastriran);
                        Console.WriteLine("\t Željena dob: {0}", o.DobZivotinje);
                    }

                    // Na dnu su podaci o korisniku, to je isto za oba tipa oglasa
                    Console.WriteLine("\t Ime korisnika: {0}", o.Korisnik.Ime);
                    Console.WriteLine("\t Prezime korisnika: {0}", o.Korisnik.Prezime);
                    Console.WriteLine("\t E-mail: {0}", o.Korisnik.Email);
                    Console.WriteLine("\t Broj mobitela: {0}", o.Korisnik.Mobitel);
                    Console.WriteLine("\t Grad: {0}", o.Korisnik.Grad);
                    Console.WriteLine("**********************************************************************************************************");

                


                }

        }


        

        

        private void PromjenaOglasa()
        {
            if (Oglasi.Count() == 0)
            {
                Console.WriteLine("\nNema oglasa na listi!\n");
               
            }
            else
            {
                PregledOglasa();    //Prvo bira oglas kojeg želi mijenjati... ili može odustati
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojeg želite promijeniti (ili 0 za povratak na izbornik): ", "Nije dobar odabir.", 0, Oglasi.Count());
                
                if (index != 0)
                {
                    var o = Oglasi[index - 1];

                    Izbornik.ObradaKorisnika.PregledKorisnika();   // Može promijeniti korisnika koji je objavio oglas (a ako izabere istog, ažurirat će se njegovi podaci ako je došlo do neke promjene)
                    int index2 = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj korisnika koji objavljuje oglas (" + o.Korisnik + "): ", "Nije dobar odabir.", 1, Izbornik.ObradaKorisnika.Korisnici.Count());
                    o.Korisnik = Izbornik.ObradaKorisnika.Korisnici[index2 - 1];

                    // šifra i datum objave idu automatski i ne mogu se mijenjati
                    // kategorija se ne može mijenjati, jer su pitanja drugačija i oglasi u kojima se traži životinja nemaju fotografije - ako se netko zeznuo, najbolje je obrisati oglas
                    o.Aktivan = Ucitavanje.UcitajBool("Je li oglas aktivan? Upišite 'da' ili bilo što drugo za ne: ");
                 

                    Console.WriteLine("\nU nastavku unesite promjene ili pritisnite tipku Enter ako ste zadovoljni s trenutnim podacima:\n");

                    o.NaslovOglasa = Ucitavanje.UcitajPromjenu("Unesite naslov oglasa (" + o.NaslovOglasa + "): ", o.NaslovOglasa);
                    o.OpisOglasa = Ucitavanje.UcitajPromjenu("Unesite opis oglasa (" + o.OpisOglasa + "): ", o.OpisOglasa);

                    if (o.Kategorija == 1)  //Ako poklanja životinju, oglas može imati fotografije
                    {
                        o.VrstaZivotinje = Ucitavanje.UcitajPromjenu("Unesite vrstu životinje (" + o.VrstaZivotinje + "): ", o.VrstaZivotinje);
                        o.ImeZivotinje = Ucitavanje.UcitajPromjenu("Unesite ime životinje (" + o.ImeZivotinje + "): ", o.ImeZivotinje);
                        o.SpolZivotinje = Ucitavanje.UcitajPromjenu("Unesite spol životinje (" + o.SpolZivotinje + "): ", o.SpolZivotinje);
                        o.Kastriran = Ucitavanje.UcitajPromjenu("Je li kastrirana? (" + o.Kastriran + "): ", o.Kastriran);
                        o.DobZivotinje = Ucitavanje.UcitajPromjenu("Unesite dob životinje (" + o.DobZivotinje + "): ", o.DobZivotinje);

                        if (o.Fotografije.Count() == 0)
                        {
                            Console.WriteLine("\nU oglasu nema fotografija.\n");
                            if (Ucitavanje.UcitajBool("Želite li dodati neku fotografiju? Upišite 'da' ili bilo što drugo za ne: "))
                            {
                                var f = new Fotografija();
                                f.Sifra = Fsifra++;
                                f.Naziv = Ucitavanje.UcitajString("Unesite naziv fotografije: ", "Naziv je obavezan.");
                                f.Link = Ucitavanje.UcitajString("Unesite link fotografije: ", "Link je obavezan.");
                                o.Fotografije.Add(f);
                                Console.WriteLine("\nFotografija je uspješno dodana u oglas.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Fotografije:");
                            int broj = 1;
                            foreach (Fotografija f in o.Fotografije)
                            {
                                Console.WriteLine("\t {0}. {1}", broj++, f);
                            }
                            if (Ucitavanje.UcitajBool("Želite li dodati ili obrisati neku fotografiju? Upišite 'da' ili bilo što drugo za ne: "))
                            {
                                int index3 = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj fotografije koju želite obrisati ili 0 za dodavanje nove fotografije: ", "Nije dobar odabir.", 0, o.Fotografije.Count());
                                if (index3 == 0)
                                {

                                    var f = new Fotografija();
                                    f.Sifra = Fsifra++;
                                    f.Naziv = Ucitavanje.UcitajString("Unesite naziv fotografije: ", "Naziv je obavezan.");
                                    f.Link = Ucitavanje.UcitajString("Unesite link fotografije: ", "Link je obavezan.");
                                    o.Fotografije.Add(f);
                                    Console.WriteLine("\nFotografija je uspješno dodana u oglas.\n");
                                }
                                else
                                {
                                    o.Fotografije.RemoveAt(index3 - 1);
                                    Console.WriteLine("\nFotografija je uspješno obrisana.\n");
                                }
                            }
                        }

                        
                        
                    }

                    else    //Ako želi udomiti životinju, pitanja su malo drugačija i nema fotografija
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
                
            }
            else
            {
                PregledOglasa();
                int index = Ucitavanje.UcitajBrojRaspon("Odaberite redni broj oglasa kojeg želite obrisati (ili 0 za izlaz): ", "Nije dobar odabir.", 0, Oglasi.Count());
                if (index != 0)
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
            o1.NaslovOglasa = "Leona traži dom!";
            o1.OpisOglasa = "Bivši vlasnici su ju napustili zbog odlaska u inozemstvo. Traži dobrog udomitelja.";
            o1.VrstaZivotinje = "Kunić";
            o1.ImeZivotinje = "Leona";
            o1.SpolZivotinje = "Ženka";
            o1.Kastriran = "Nije";
            o1.DobZivotinje = "Oko 6 mjeseci";
            o1.Fotografije = new List<Fotografija>();
            o1.Fotografije.Add(new Fotografija
            {
                Sifra = Fsifra++,
                Naziv = "Leona",
                Link= "https://i.postimg.cc/N0zqmfMj/Leona.jpg"
            });
            o1.Poruke = new List<Poruka>();
            o1.Poruke.Add(new Poruka
            {
                Sifra = 1,
                ImePosiljatelja = "Tihana", 
                EmailPosiljatelja = "tihana.hajsok@gmail.com", 
                TekstPoruke = "Može prijevoz do Osijeka?",
                DatumPoruke = DateTime.Parse("23.03.2023")
            });
            Oglasi.Add(o1);



            var o2 = new Oglas();
            o2.Sifra = 2;
            o2.Aktivan = true;
            o2.Korisnik = Izbornik.ObradaKorisnika.Korisnici[0];
            o2.Kategorija = 1;
            o2.DatumObjave = DateTime.Parse("25.04.2023");
            o2.NaslovOglasa = "Dixie traži novi dom!";
            o2.OpisOglasa = "Bivši vlasnik ga je predao udruzi jer se djeca više ne žele brinuti za njega.";
            o2.VrstaZivotinje = "Zamorčić";
            o2.ImeZivotinje = "Dixie";
            o2.SpolZivotinje = "Mužjak";
            o2.Kastriran = "Nije";
            o2.DobZivotinje = "1 godinu";
            o2.Fotografije = new List<Fotografija>();
            o2.Fotografije.Add(new Fotografija
            {
                Sifra = Fsifra++,
                Naziv = "Dixie",
                Link = "https://i.postimg.cc/mrRGB6nT/Dixie.jpg"
            });
            o2.Poruke = new List<Poruka>();
            o2.Poruke.Add(new Poruka
            {
                Sifra = 2,
                ImePosiljatelja = "Goran",
                EmailPosiljatelja = "goran.kos@gmail.com",
                TekstPoruke = "Javljam se za Dixija, tražim društvo mom Pixiju. Ima 4 mjeseca.",
                DatumPoruke = DateTime.Parse("23.03.2023")
            });
            Oglasi.Add(o2);



            var o3 = new Oglas();
            o3.Sifra = 3;
            o3.Aktivan = true;
            o3.Korisnik = Izbornik.ObradaKorisnika.Korisnici[1];
            o3.Kategorija = 1;
            o3.DatumObjave = DateTime.Parse("08.05.2023");
            o3.NaslovOglasa = "Mambo traži dom!";
            o3.OpisOglasa = "Mladi kunić nađen na parkingu u Osijeku, traži dobrog udomitelja.";
            o3.VrstaZivotinje = "Kunić";
            o3.ImeZivotinje = "Mambo";
            o3.SpolZivotinje = "Mužjak";
            o3.Kastriran = "Da";
            o3.DobZivotinje = "Oko 5 mjeseci";
            o3.Fotografije = new List<Fotografija>();
            o3.Fotografije.Add(new Fotografija
            {
                Sifra = Fsifra++,
                Naziv = "Mambo",
                Link = "https://i.postimg.cc/dtvX7TB9/Mambo.jpg"
            });
            o3.Poruke = new List<Poruka>();
            o3.Poruke.Add(new Poruka
            {
                Sifra = 3,
                ImePosiljatelja = "Marina",
                EmailPosiljatelja = "marina.simlesa@gmail.com",
                TekstPoruke = "Javljam se za Mamba, imam curu Lily kojoj tražim društvo. Iz Osijeka sam.",
                DatumPoruke = DateTime.Parse("10.05.2023")
            });
            Oglasi.Add(o3);


            var o4 = new Oglas();
            o4.Sifra = 4;
            o4.Aktivan = true;
            o4.Korisnik = Izbornik.ObradaKorisnika.Korisnici[5];
            o4.Kategorija = 2;
            o4.DatumObjave = DateTime.Parse("23.05.2023");
            o4.NaslovOglasa = "Sheldon traži društvo!";
            o4.OpisOglasa = "Tražimo mužjaka kako Miki više ne bi bio sam. Ima 1 godinu.";
            o4.VrstaZivotinje = "Zamorčić";
            o4.ImeZivotinje = "dugodlaki";
            o4.SpolZivotinje = "Mužjak";
            o4.Kastriran = "Nije važno";
            o4.DobZivotinje = "Do 1 godinu";
            o4.Poruke = new List<Poruka>();
            o4.Poruke.Add(new Poruka
            {
                Sifra = 4,
                ImePosiljatelja = "Josipa",
                EmailPosiljatelja = "josipa.kovac@gmail.com",
                TekstPoruke = "Pozdrav! Imam bebu mužjaka za pokloniti, star je oko 3 mjeseca.",
                DatumPoruke = DateTime.Parse("25.05.2023")

            });
            Oglasi.Add(o4);


            var o5 = new Oglas();

            o5.Sifra = 5;
            o5.Aktivan = true;
            o5.Korisnik = Izbornik.ObradaKorisnika.Korisnici[4];
            o5.Kategorija = 2;
            o5.DatumObjave = DateTime.Parse("08.06.2023");
            o5.NaslovOglasa = "Tražim činčilu!";
            o5.OpisOglasa = "Želim udomiti činčilu, po mogućnosti mladog mužjaka.";
            o5.VrstaZivotinje = "Činčila";
            o5.ImeZivotinje = "dugorepa";
            o5.SpolZivotinje = "Nije važno";
            o5.Kastriran = "Nije važno";
            o5.DobZivotinje = "Do 1 godinu";
                o5.Poruke = new List<Poruka>();
            o5.Poruke.Add(new Poruka 
            {
                Sifra = 5,
                ImePosiljatelja = "Mirela",
                EmailPosiljatelja = "mirela.loncar@gmail.com",
                TekstPoruke = "Pozdrav! Ako još niste udomili, imam mladu ženkicu, staru oko 6 mjeseci.",
                DatumPoruke = DateTime.Parse("15.06.2023")
            });
            Oglasi.Add(o5);




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
                DobZivotinje = "Do 3 mjeseca",
                Poruke = new List<Poruka>()
            });
            

        }
        
        

    }
}
