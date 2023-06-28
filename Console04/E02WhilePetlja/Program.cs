


//while radi s bool tipom podataka

// beskonačna while petlja:

//while(true)
// { 
// }


// u while petlju se ne mora ući

while (false)
{
    Console.WriteLine("02 Nisam ušao");
}




int b = 0;
while (b < 10)
{ 
Console.WriteLine(++b); // prvo uvećaj b, onda ga koristi
}

// ispisat će brojeve od 1 do 10




int t = 2;
b = 0;
while (t == 2 && b < 10)    
{
    Console.WriteLine(++b);
}


b = 0;
while (true)
{
    if (b == 2)
    {
        b++;
        continue;
    }
    if (b == 5)
    {
        break;
    }
    Console.WriteLine(b++);
}

// ispisat će 0, 1, 3, 4


// Program unosi broj između 1 i 10
// Program ispisuje uneseni broj

while (true)
{
    Console.Write("Unesi cijeli broj: ");
    b = int.Parse(Console.ReadLine());
    if (b > 0 && b <= 10)
    {
        break;
    }
    Console.WriteLine("Morate unijeti broj između 1 i 10");
}
Console.WriteLine("Uneseni broj je: {0}", b);


//  Petlja se izvodi sve dok korisnik ne unese broj između 1 i 10



// Napišite program koji pomoću while petlje ispisuje svaki treći broj između 2 i 97


int n = 2;
while (n <= 97)
{
    Console.WriteLine(n);
    n = n + 3;      //  može i n+=3;
}

// 2. rješenje

b = 2;
while (true)
{
    Console.WriteLine(b);
    b += 3;
    if (b > 97)
    {
        break;
    }
}



// 3. zadatak
// zbrojite prvih 100 brojeva s while petljom

int i = 1;
int zbroj = 0;
while (i <= 100)
{
    zbroj += i;
    i++;
}
Console.WriteLine("Zbroj prvih 100 brojeva je: {0}", zbroj);



zbroj = 0;
b = 0;
while (b++ < 100)
{
    zbroj += b;
}
Console.WriteLine(zbroj);




//ugnježđivanje i prekidanje unutarnjih petlji je isto kao u for




