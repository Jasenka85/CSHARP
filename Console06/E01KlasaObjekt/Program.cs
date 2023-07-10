
using E01KlasaObjekt;

string ime = "Pero";
string prezime = "Perić";
int godine = 24;

ime = "marija";    // Peru smo izgubili

string ime1 = "marija";     // ni ovo nije dobro rješenje

// mogli bi napraviti niz imena, ali ni to nije rješenje

// princip klase i objekta

// Ovo morate znati nakon svatova u 4 ujutro
// Objekt je instanca/pojavnost klase
// Klasa je opisnik objekta
// Klasa je kao nacrt, a objekt kao konkretna kuća koja je izgrađena po tom nacrtu

// Osoba je naziv klase (tip podatka)
// o je instanca klase, o je objekt, o je varijabla
// new je ključna riječ koja poziva koja poziva posebnu metodu: konstruktor
// ona se poziva u trenutku instanciranja novog objekta

Osoba o = new Osoba();

o = new Osoba("Pero");

// eksplicitni način deklariranja varijabli
Osoba natjecatelj = new Osoba();

// implicitni način deklariranja varijabli
var prijavitelj = new Osoba("Marija");  // s lijeve strane nema tip -> desna strana određuje koji je tip!

var i = 1;

//Ovo je drugi dio zadatka Z01

Dokument[] dokumenti = new Dokument[3];

dokumenti[0] = new Dokument();  //članovi niza su nam instance klase 
dokumenti[1] = new Dokument();
dokumenti[2] = new Dokument("Račun");

// Ovo je drugi dio zadatka Z02

Smjer smjer = new Smjer("Web programiranje", 1432);


Grupa grupa;

for (int j = 0; j < 128; j++)
{
    grupa = new Grupa();
}

