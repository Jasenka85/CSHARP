using System.Buffers;
using E01Nasljedivanje;

var o = new Osoba
{
    Sifra = 1,
    Ime = "Pero",
    Prezime = "Perić"

};


Console.WriteLine(o.Ime);       //ispisat će Pero

Console.WriteLine(o);       //ispiši cijeli objekt o -> izvede se metoda ToString

// dobit ćemo samo  E01Nasljedivanje.Osoba -> namespace i klasa


var drugaOsoba = new Osoba
{

    Sifra = 1,
    Ime = "Marina",
    Prezime = "Marić"
};

Console.WriteLine(o.Equals(drugaOsoba));        //ispisat će True


var p = new Polaznik
{
    Sifra = 1,
    Ime = "Marko",
    Prezime = "Kat",
    BrojUgovora = "2023/56"

};

Console.WriteLine(p);
// ispisat će Marko Kat, kao što smo definirali u klasi Osoba
// pošto na Polaznik.cs nema metode ToString(), ide u nadklasu Osoba.cs -> ako nema ni tamo, ide u klasu Object 



//Ako želimo ispisati i ime ugovora, stavit ćemo u Polaznik metodu ToString


var pr = new Predavac
{
    Sifra = 1,
    Ime = "Rita",
    Prezime = "Man",
    Iban = "HR458585255555"

};

Console.Write(pr);
//Ispisat će samo ime i prezime, jer nasljeđuje klasu Osoba i nema definiran override ToString


var pas = new Pas
{
   Koljeno = "Svitkovci",
   Red= "Zvijeri",
   Pasmina= "Hrvatski ovčar"
};


var som = new Som
{ 
Koljeno="Svitkovci",
TipVode="Slatkovodna",
Staniste="Dunav"
};



