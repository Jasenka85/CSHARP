

// 1. metoda tipa void, koja ne prima parametre

// deklaracija metode

using ZajednickeMetode;

void Tip1()
{
    Console.WriteLine("Dobrodošli u program");
}

// poziv metode

Tip1();

// ako deklariramo metodu, a ne pozovemo ju, ona se neće izvesti

// 2. metoda tipa void, koja prima parametre

void Tip2(string poruka)        // varijabla je parametar metode
{
    Console.WriteLine(poruka);      //ispisuje ono što primi
}

Tip2("Ovo je vrijednost koju prosljeđujem");

// drugi način pozivanja

string s = "Vrijednost varijable s";

Tip2(s);    // varijablu proslijedimo metodi     


// 3. određenog tipa, ne prima vrijednost

int Tip3()
{
    return new Random().Next();     //ako je metoda određenog tipa, mora imati return
}

Tip3();  //on će vratiti slučajni broj, ali kod s tim brojem ništa ne radi


Console.WriteLine(Tip3());      // možemo ga recimo ispisati

int sb = Tip3();            // ili spremiti u neku varijablu

Console.WriteLine(sb);



// NAJVAŽNIJA 4. određenog tipa i prima parametre

int Tip4(int min, int max)
{
    int manji = min < max ? min : max;
    int veci = max > min ? max : min;
    sb = 3;         
    return new Random().Next(manji,veci);          
}

Console.WriteLine(Tip4(20, 30));
Console.WriteLine(Tip4(-20, -30));

// specifičnost top-level statements načina

void ispis()
{
    Console.WriteLine("Hello world");
}

// void ispis(string poruka)               // potpis metode je naziv+lista parametara
// {                                       // ovo se ne može u top level statements
// }

// instanca klase Metode

Metode m = new Metode();

m.ispis();      // poziv metode


// metoda koja zbraja dva unesena broja

Console.WriteLine(Metode.izracunaj(2, 8));



// pozivamo 3 različite verzije metode ispis

m.ispis();
m.ispis("12");
m.ispis(12);

m.ispis();


// metoda za ispisivanje matrice


int[,] t = new int[5, 5];

Metode.ispisiMatricu(t);

t[2, 4] = 7;

Metode.ispisiMatricu(t);


// metoda za računanje faktorijela

Console.WriteLine(Metode.faktorijel(5));




