
using E02ApstraktnaKlasaIMetoda;

// var o = new Osoba();        //Ovo ne možemo, jer apstraktnu klasu ne možemo instancirati
//Njih koristimo kao nadklase, možemo ih nasljeđivati

var p = new Polaznik
{
    Ime = "Pero",
    Spol = "Muško"
};


var pr = new Predavac
{
    Ime = "Mario",
    Godine = 24

};

void ispis(Osoba o)
{
    o.Pozdravi();
}

//Metoda ispis prima 1 parametar tipa Osoba
// šaljemo mu instancu klase Polaznik, koja nasljeđuje klasu Osoba

ispis(p);

// Također možemo poslati instancu klase Predavac, jer i ona nasljeđuje Osobu
ispis(pr);