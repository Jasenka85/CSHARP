// deklaracija varijable

int i;

//  dodjeljivanje vrijednosti

i = 4;

// korištenje varijable

Console.WriteLine(i);


string nizZnakova = "Edunova";

bool logickaVrijednost = true;

float decimalniBroj = 3.14f;

double decimalniBrojVeci = 3.14;

decimal db = 3.14m;


Console.WriteLine("decimalni broj: {0}", decimalniBroj);

Console.WriteLine("decimalni broj: {0}\ndb: {1}", decimalniBroj, db);


// Raspon

Console.WriteLine(int.MinValue);
Console.WriteLine(int.MaxValue);

int b = int.MaxValue;
b = b + 1;
Console.WriteLine(b);


// Zadatak 1
// Deklarirajte 4 varijable različitih tipova podataka
// Svakom od njih dodijelite vrijednost po želji
// Sve ih odjednom ispišite jedno pored drugog

string adresa = "Sjenjak";
int kucniBroj = 103;
decimal brojKilometara = 49.54m;
bool vozioDanas = true;

Console.WriteLine("{0},{1},{2},{3}", adresa, kucniBroj, brojKilometara, vozioDanas);


// Tips

int a, q, w = 3;
bool istina = w == 3;   // operator uspoređivanja

Console.WriteLine(istina);


