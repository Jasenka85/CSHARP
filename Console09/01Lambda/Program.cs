
// napišite metodu tipa int naziva KlasicnaMetoda 
// koja prima parametar tipa int i vraća primljeni broj na kvadrat


using _01Lambda;

int KlasicnaMetoda(int b)
{
    return b * b;
}

//u konzolu ispišite poziv definirane metode s parametrom 5


Console.WriteLine(KlasicnaMetoda(5));

// lambda expression

var kvadrat = (int b) => b * b;   //=> je lambda operator

Console.WriteLine(kvadrat(5));

// lambda statement

var algoritam = (int x, int y) =>
{
    var t = x++ + --y;
    return x + y - t;
};

Console.WriteLine(algoritam(1,2));


int[] brojevi = { 2, 3, 4, 3, 2, 3, 4, 3 };

// prebrojite koliko ima brojeva s vrijednoscu 3 u nizu

int ukupno = 0;

foreach (int b in brojevi)
{
    if (b == 3)
    {
        ukupno++;
    }
}
Console.WriteLine(ukupno);

// Brži način

Console.WriteLine(brojevi.Count(b=> b==3));


for (int i = 0; i < brojevi.Length; i++)
{
    Console.WriteLine(brojevi[i]);
}

Console.WriteLine("-----");

foreach (int k in brojevi)
{
    Console.WriteLine(k);
}

Console.WriteLine("-----");

// Brži način, koji zamjenjuje obje petlje

Array.ForEach(brojevi, Console.WriteLine);

Console.WriteLine("-----");


// ispisati svaki broj uvećan za 1

Array.ForEach(brojevi, b =>
{
    Console.WriteLine(b + 1);
});


// deklarirajte listu sa sljedećim elementima: 2,3,4,5,4

var lista = new List<int>() { 2, 3, 4, 5, 4 };

// drugi način 

List<int> l = new() { 2, 3, 4, 5, 4 };

lista.ForEach(Console.WriteLine);

var smjerovi = new List<Smjer>() 
{
new() { Naziv="Prvi", Sifra=1},
new() { Naziv="Drugi", Sifra=2}
};

smjerovi.ForEach(Console.WriteLine);

smjerovi.ForEach(s =>
{
    int b = s.Sifra + new Random().Next();
    Console.WriteLine(s.Naziv+ " " + b);
    
});


string s = "Edunova";

Console.WriteLine(s?.Replace("a","b"));

// Replace neće raditi ako je s null
// Taj problem rješavamo s ?, jer ako je s null,
// zamijenit će ga s praznim stringom i onda Replace prolazi