int[] niz = { 2, 3, 3, 4, 5, 5 };

for (int i = 0; i < niz.Length; i++)
{
    Console.WriteLine(niz[i]);
}


// ispisuje članove niza od prvog do zadnjeg


for (int i = niz.Length - 1; i >= 0; i--)
{
    Console.WriteLine(niz[i]);
}

// ispisuje članove niza od zadnjeg do prvog




//Ako trebamo ispisati od prvog do zadnjeg člana, možemo koristi For Each petlju

foreach (int en in niz)
{
    Console.WriteLine(en);
}

// nedostatak je što ne možemo ići unazad


// ugnježđivanje, nastavak i prekidanje je isto kao u for, while i do


// Zadatak Ciklična tablica (matrica)

