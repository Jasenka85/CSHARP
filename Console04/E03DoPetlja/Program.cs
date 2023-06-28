
// do petlja osigurava minimalno jedno izvođenje, zato što je uvjet na kraju petlje

do
{
    Console.WriteLine("01 Ušao u petlju");
}
while (false);

// da ovdje stavimo while(true) to bi bila beskonačna petlja


// ostalo je isto kao u for i while petlji


// Korisnik unosi 2 cijela broja između 10 i 20
// Program ispisuje sve parne brojeve između unesenih brojeva
// Koristiti do petlju


int br1 = 0;
int br2 = 0;

do
{
    Console.Write("Unesi prvi cijeli broj: ");
    br1 = int.Parse(Console.ReadLine());
    if (br1 >= 10 && br1 <= 20)
    {
        break;
    }
    Console.WriteLine("Broj nije između 10 i 20!");
}
while (true);

do
{
    Console.Write("Unesi drugi cijeli broj: ");
    br2 = int.Parse(Console.ReadLine());
    if (br2 >= 10 && br2 <= 20)
    {
        break;
    }
    Console.WriteLine("Broj nije između 10 i 20!");
}
while (true);

int manji = br1 < br2 ? br1 : br2;
int veci = br1 > br2 ? br1 : br2;
int i = manji;
do
{
    if (i % 2 == 0)
    {
        Console.WriteLine(i);
    }
}
while (++i <= veci);