int i = 1;
bool uvjet = i > 0;
if (uvjet)
{
    Console.WriteLine("1: Veće od 0");
}

// Koristit ćemo ovu sintaksu
if (i > 0)
{
    Console.WriteLine("2: Opet veće od 0");
}

// Jedna od većih grešaka
if (uvjet == true)
{

}


// Ako se if odnosi na jednu liniju, ne trebaju vitičaste zagrade

if (!uvjet)
    Console.WriteLine("3: Nije veće od 0");
Console.WriteLine("4: Ovo isto ne bi trebalo biti veće od 0");

//drugi red se neće izvesti!


// opcionalna sintaksa
string grad = "Osijek";
if (grad == "Osijek")
{
    Console.WriteLine("5: SUPER");

}
else
{
    Console.WriteLine("6: OK");
}


int b = 0;
if (grad != "Zagreb")
{
    b = b + 1;  // broj b se uvećava za 1

}
else if (grad == "Split")
{
    b += 1;     // broj b se uvećava za 1

}
else if (grad == "Osijek")
{
    b += 2;     // broj b se uvećava za 2
}
else
{
    b++;        // broj b se uvećava za 1
}

Console.WriteLine("7:" + b);



// if možemo ugnijezditi

i = 5; b = 2;
if (i > 0)
{
    if (b == 2)
    {
        Console.WriteLine("8: Oba uvjeta su zadovoljena");
    }
}

// Ispisat će se ako su oba uvjeta zadovoljena


// Ista stvar, ali uz korištenje logičkih operatora

if (i > 0 & b == 2)
{
    Console.WriteLine("9: Oba uvjeta su zadovoljena");
}


// Jedan & provjerava oba uvjeta  (AND)

if (i > 0 && b == 2)
{
    Console.WriteLine("10: Oba uvjeta su zadovoljena");
}

// && neće provjeravati drugi uvjet ako prvi nije zadovoljen



//  znak | je ILI  (AltGr + W)
//  ako stavimo ||, ukoliko je prvi uvjet zadovoljen, drugi se ne mora provjeravati

if (i == 4 || b < 0)
{


}


// Znak za NOT je !



int g = 10;
if (g % 2 == 0)     // ostatak pri djeljenju s 2 je 0
{
    Console.WriteLine("11: Broj je paran");
}
else
{
    Console.WriteLine("12: Broj je neparan");
}


// Ovo može kraće
// ? operator - inline if

Console.WriteLine("Broj je " + (g % 2 == 0 ? "" : "ne") + "paran");

// uvjet, pa nakon ? ide true dio, a nakon : false dio


// 1. Zadatak
//Korisnik unosi cijeli broj.
//Za broj manji od 10 ispisuje se Osijek
//inače se ispisuje Donji Miholjac

Console.Write("Unesi jedan cijeli broj: ");
int broj = int.Parse(Console.ReadLine());
if (broj < 10)
{
    Console.WriteLine("Osijek");
}
else
{
    Console.WriteLine("Donji Miholjac");
}

// kraće:
Console.WriteLine(broj < 10 ? "Osijek" : "Donji Miholjac");


// 2. zadatak
// Korisnik unosi cijeli broj
// Program ispisuje je li paran ili nije


Console.WriteLine("Unesi jedan prirodan broj: ");
broj = int.Parse(Console.ReadLine());
Console.WriteLine("Broj je " + (broj % 2 == 0 ? "" : "ne") + "paran");



// 3. zadatak
// Za dva unesena cijela broja
// program ispisuje Osijek
// ako je zbroj veći od 10
// inače ispisuje Edunova

Console.Write("Unesi prvi cijeli broj: ");
int prvibroj = int.Parse(Console.ReadLine());

Console.Write("Unesi drugi cijeli broj: ");
int drugibroj = int.Parse(Console.ReadLine());

Console.WriteLine(prvibroj + drugibroj > 10 ? "Osijek" : "Edunova");








