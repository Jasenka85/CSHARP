
int redaka, stupaca;
Console.Write("Unesi broj redova: ");
redaka = int.Parse(Console.ReadLine());
Console.Write("Unesi broj stupaca: ");
stupaca = int.Parse(Console.ReadLine());

int[,] matrica = new int[redaka, stupaca];

for (int i = 0; i < redaka; i++)
{
    for (int j = 0; j < stupaca; j++)
    {
        Console.Write(matrica[i, j]+" ");
    }
    Console.WriteLine();
}



// Matrica je spremna, svi elementi su 0
// Treba nam brojač koji će se povećavati za 1


Console.WriteLine("***************************");

// Za početak probamo ručno unijeti par elemenata:

int b = 1;

/*
matrica[redaka - 1, stupaca - 1] = b++;       // zadnji element u zadnjem redu nam je 1, nakon toga se b uveća
matrica[redaka - 1, stupaca - 2] = b++;       // predzadnji element u zadnjem redu nam je 2, opet uvećaj b
matrica[redaka - 1, stupaca - 3] = b++;
matrica[redaka - 1, stupaca - 4] = b++;
matrica[redaka - 1, stupaca - 5] = b++;       // za matricu 5x5 ovo je prvi element u zadnjem redu, dalje ne možemo ići

// dakle, možemo ići do stupaca-stupaca, dalje ne može

*/

for (int i = 1; i <= stupaca; i++)
{
    matrica[redaka - 1, stupaca - i] = b++;
}



for (int i = 0; i < redaka; i++)
{
    for (int j = 0; j < stupaca; j++)
    {
        Console.Write(matrica[i, j] + " ");
    }
    Console.WriteLine();
}


for (int i = redaka-2; i >= 0; i--)
{
    matrica[i, 0] = b++;
}


for (int i = 0; i < redaka; i++)
{
    for (int j = 0; j < stupaca; j++)
    {
        Console.Write(matrica[i, j] + " ");
    }
    Console.WriteLine();
}


// petlja while završava kada b dođe do broja redaka x stupaca