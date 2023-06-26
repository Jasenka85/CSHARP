// DZ
// Za uneseni dvoznamenkasti broj
// ispišite jediničnu vrijednost
// unos 21, ispis 1
// unos 87, ispis 7

Console.Write("Unesi jedan dvoznamenkasti prirodan broj: ");
int dvbroj = int.Parse(Console.ReadLine());
int ostatak = dvbroj % 10;
Console.WriteLine("Jedinična vrijednost je: {0}",ostatak);
