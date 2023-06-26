// 10 puta ispiši Osijek
// najgore rješenje

// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");
// Console.WriteLine("Osijek");

// for(od kuda;do kuda;korak)

Console.WriteLine("1. ****************");

for (int i = 0; i < 10; i = i + 1)
{
    Console.WriteLine("Osijek");
}

Console.WriteLine("2. ****************");

int j = 0;
for (j = 10; j > 0; j--)
{
    Console.WriteLine("Edunova");
}

// DZ vježbati s Break point i debug

Console.WriteLine("3. ****************");

for (int i = 0; i < 10; i=i+2)
{
    Console.WriteLine("CSHARP");
}


// varijabla u petlji mijenja vrijednost!!!

Console.WriteLine("4. ****************");

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}

Console.WriteLine("5. ****************");

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i+1);     // i+1 ne mijenja vrijednost varijable
}

Console.WriteLine("6. ****************");


for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);     // ne koristiti!
}

Console.WriteLine("7. ****************");

// zbroj prvih 100 brojeva

int zbroj = 0;

for (int i = 1; i <= 100; i++)
{
    // Console.WriteLine(i);
    zbroj += i;   //zbroj=zbroj+i
    // Console.WriteLine(zbroj);
}

Console.WriteLine(zbroj);





Console.WriteLine("8. ****************");

// Ispisati sve parne brojeve od 1 do 57

for (int i = 1; i <= 57; i++)
{
    if (i % 2 == 0)
    {
        Console.WriteLine(i);
    }
}

Console.WriteLine("9. ****************");

// ispiši zbroj svih parnih brojeva od 2 do 18

zbroj = 0;
for (int i = 2; i <= 18; i++)
{
    if (i % 2 == 0)
    {
        zbroj+=i;
    }
}

Console.WriteLine(zbroj);



Console.WriteLine("10. ****************");


int[] niz = { 2, 2, 34, 54, 5, 6, 76, 7, 8, 9, 13};

// jedno ispod drugog ispisati svaki element niza

for (int i = 0; i < niz.Length; i++)
{
    Console.WriteLine(niz[i]);
}


// Program učitava koliko će se brojeva unijeti
// Program učitava brojeve za provjeru
// Program ispisuje najveći uneseni broj

Console.Write("Unesi koliko brojeva provjeravaš: ");
int brojeva = int.Parse(Console.ReadLine());
int broj;           // nikada ne deklarirati varijablu unutar petlje, uvijek izvan!
int najveci= int.MinValue;  // prije početka mu dodijelimo najmanju vrijednost
for (int i = 0; i < brojeva; i++)
{
    Console.Write("Unesi {0}. broj: ",i+1);
    broj = int.Parse(Console.ReadLine());
    if (broj > najveci)
    {
        najveci = broj;
    }
}
Console.WriteLine("Najveći broj je {0}",najveci);



// ovo nije beskonačna petlja, jer poslije najvećeg broja odlazimo u minus

// for (int i = 1; i > 0; i++)
// { 
// }


// ovo je beskonačna petlja, jer dijelovi u zagradi nisu obvezni

//for (; ;)
// {
//    Thread.Sleep(1000);
//    Console.WriteLine(new Random().NextDouble());    // loša sintaksa
//    break;        // nasilno prekidanje petlje
// }



Console.WriteLine("12.  ****************");

for (int i = 0; i < 10; i++)
{
    if (i == 3)
    {
        break;
    }
    Console.WriteLine(i);
}


// petlja se može nastaviti/preskočiti

Console.WriteLine("13.  ****************");

for (int i = 0; i < 10; i++)
{
    if (i == 2 || i==5)
    {
        continue;
    }
    Console.WriteLine(i);
}


// ugnježđivanje petlji

Console.WriteLine("14.  ****************");
string s;
for (int i = 1; i <= 10; i++)        //dok je i=1, k se prošeće od 1 do 9
{
    for (int k = 1; k <= 10; k++)
    {
        s = "    " + i * k;
        Console.Write(s[^4..]);       // ispisat će tablicu množenja, lijepo poravnato
                                      // string je niz znakova, tražimo zadnja 4 znaka
    }
    Console.WriteLine();        // kada završi unutarnja petlja, prijeći će u novi red
}


// DZ  TABLICA MNOŽENJA - vidi na Gitu


// nasilno prekidanje unutarnjih petlji

for (int i = 0; i < 10; i++)
{
    for (int k = 0; k < 10; k++)
    {
        // break;  // ovo prekida samo unutarnju petlju, ne i vanjsku
        goto labela;    // ovo će prekinuti i unutarnju i vanjsku petlju
        // ne mora se zvati labela, može biti bilo koji naziv
    }
}

labela:;


// 9 različitih načina zbrajanja prvih 100 brojeva - vidi na Gitu


// 1. zadatak
// Korisnik unosi 2 cijela broja
// Program ispisuje zbroj svih parnih brojeva 
// između ta dva broja


Console.WriteLine("Unesi prvi prirodan broj: ");
int pb = int.Parse(Console.ReadLine());
Console.WriteLine("Unesi drugi prirodan broj: ");
int db = int.Parse(Console.ReadLine());

int manji = pb < db ? pb : db;  // Ako je prvi broj manji od drugog, uzmimamo prvi, inače uzimamo drugi
int veci = pb > db ? pb : db;
for (int i = manji; i <= veci; i++)
{
    zbroj = i % 2 == 0 ? zbroj + i : zbroj;    // Ako je broj i paran, dodati ga u zbroj
}
Console.WriteLine(zbroj);


// U for petlju se ne mora ući!

int t = 100;
for (int i = 0; i > t; i++)     //  nije ušao jer i nije veće od t
{
    Console.WriteLine("Ušao u petlju");
}


// 2. zadatak
// Program ispisuje brojeve od 100 do 1 u istom redu odvojeno zarezom


for (int i = 100; i > 0; i--)
{
    Console.Write(i+ (i!=1 ? "," : ""));        // ovime osiguravamo da nakon 1 ne piše zarez
}

