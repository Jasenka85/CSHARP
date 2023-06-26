
// Operatori uspoređivanja

bool razlicito = 3 != 4;

bool vece = 8 > 6;

Console.WriteLine("{0},{1}",razlicito, vece);

// Logički operatori AND, OR, NOT

bool rez = razlicito & vece;

// Operator + ima dvojaku ulogu

string s = "Edunova" + " Osijek";

int x = 6 + 2;

string s1 = "Broj " + 5;

// Operator modulo % - ostatak nakon cjelobrojnog dijeljenja
// školski primjer su parni i neparni brojevi
// Za uneseni broj ispiši True ako je paran, a False ako je neparan

Console.Write("Unesi broj: ");
x = Int16.Parse(Console.ReadLine());

Console.WriteLine(x % 2 == 0);

// Deklarirajte varijablu tipa int naziva negativniBroj
// i dodijelite joj vrijednost -262

int negativniBroj = -262;


// ispišite pozitivni ekvivalent od negativniBroj

Console.WriteLine(negativniBroj * (-1));

// Za unesena dva cijela broja unesite rezultat dijeljenja
// npr. ulaz 5 i 10, izlaz 0.5

Console.Write("Unesi prvi cijeli broj: ");
int cb1 = int.Parse(Console.ReadLine());
Console.Write("Unesi drugi cijeli broj: ");
int cb2=int.Parse(Console.ReadLine());
float rezultat = cb1 / (float)cb2;
Console.WriteLine("Rezultat dijeljenja je {0}.",rezultat);




