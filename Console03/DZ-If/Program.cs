// Domaća zadaća

// Za unesena dva cijela broj
// program ispisuje veći
// 3 i 5 -> 5
// 5 i 3 -> 5
// 5 i 5 ->


Console.Write("Unesi prvi cijeli broj: ");
int prvibroj = int.Parse(Console.ReadLine());

Console.Write("Unesi drugi cijeli broj: ");
int drugibroj = int.Parse(Console.ReadLine());

Console.WriteLine("Veći broj je: " + (prvibroj > drugibroj ? prvibroj : drugibroj));



// Za upisana 3 cijela broja
// program ispisuje najveći


Console.Write("Unesi prvi cijeli broj: ");
int broj1 = int.Parse(Console.ReadLine());

Console.Write("Unesi drugi cijeli broj: ");
int broj2 = int.Parse(Console.ReadLine());

Console.Write("Unesi treći cijeli broj: ");
int broj3 = int.Parse(Console.ReadLine());

if (broj1 > broj2 && broj1 > broj3)
{
    Console.WriteLine("Najveći broj je: " + broj1);
}
else if (broj2 > broj1 && broj2 > broj3)
{
    Console.WriteLine("Najveći broj je: " + broj2);
}
else 
{
    Console.WriteLine("Najveći broj je: " + broj3);
}


// program unosi broj između 
// 1 i 999
// U slučaju da je izvan tog raspona
// ispisati grešku i prekinuti program
// Program ispisuje 1. znamenku upisanog broja

// -5 greška
// 1067 greška
// 456 4
// 6 6
// 89 8


Console.Write("Unesi prirodan broj izmedu 1 i 999: ");
int broj4 = int.Parse(Console.ReadLine());
if (broj4 < 1 || broj4 > 999)
{
    Console.WriteLine("Greška! Prekidam...");
}
else if (broj4 < 10)
{
    Console.WriteLine("Prva znamenka tvog broja je: " + broj4);
}
else if (broj4 < 100)
{
    Console.WriteLine("Prva znamenka tvog broja je: " + broj4 / 10);
}
else
{
    Console.WriteLine("Prva znamenka tvog broja je: " + broj4 / 100);
}



// Program unosi 2 broja
// Ako su oba broja parni
// ispisuje njihov zbroj
// inače ispisuje njihovu razliku


Console.Write("Unesi prvi prirodan broj: ");
int pb1 = int.Parse(Console.ReadLine());

Console.Write("Unesi drugi prirodan broj: ");
int pb2 = int.Parse(Console.ReadLine());

if (pb1 < 0 || pb2 < 0)
{
    Console.WriteLine("Greška! Broj nije prirodan.");
}
else if (pb1 % 2 == 0 && pb2 % 2 == 0)
{
    Console.WriteLine("Njihov zbroj je: " + (pb1 + pb2));
}
else
{
    Console.WriteLine("Njihova razlika je: " + (pb1 - pb2));
}
