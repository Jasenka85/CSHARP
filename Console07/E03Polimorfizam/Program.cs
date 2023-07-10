

//Svojstvo klase da definira neko ponašanje koje ne zna kako će se izvoditi, ali zna da mora postojati
// Npr. knjiženje računa i amortizacije - nije isto, ne znamo kako se točno radi, ali znamo da se mora

using E03Polimorfizam;


Osoba[] osobe = new Osoba[3];

osobe[0] = new Polaznik {Ime = "Pero"};
osobe[1] = new Polaznik { Ime = "Kata" };
osobe[2] = new Predavac { Ime = "Mata" };

void pozdraviSve(Osoba[] o)
{
    foreach (Osoba osoba in o)
    {
        // Ovo je izvedba polimorfizma
        Console.WriteLine(osoba.Pozdravi());
    }

}

pozdraviSve(osobe);

//Tri klase Osoba, Predavac i Polaznik su pozornica za polimorfizam

