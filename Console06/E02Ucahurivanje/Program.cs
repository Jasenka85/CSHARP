using E02Ucahurivanje;

Osoba osoba = new Osoba();

osoba.setIme("Pero");
osoba.Prezime = "Perić";  // ovako ćemo koristiti u nastavku

Console.WriteLine("{0} {1}", osoba.Prezime, osoba.getIme());


Smjer smjer = new Smjer();

smjer.Sifra = 1;
smjer.Naziv = "Web programiranje";
smjer.Trajanje = 250;

//itd. ostala svojstva

// postoja brža sintaksa

smjer = new Smjer     //umjesto oblih idu vitičaste zagrade, iza vitičaste ;
{
    Sifra = 1,
    Naziv = "Java programiranje"
    // itd... ostale vrijednosti
};


Zupanija zupanija = new Zupanija
{
    Naziv = "Osječko baranjska",
    Zupan = "Anušić"
};

Grad grad = new Grad
{
    Naziv = "Osijek",
    zupanija = zupanija
};

//ispis svojstava kada jedna klasa sadrži instancu druge klase

Console.WriteLine("Grad je {0}, županija je {1}", grad.Naziv, grad.zupanija.Naziv);

