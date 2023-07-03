



int[] StringuNiz(string z)
{
    int p = z.Length;
    int[] napraviniz = new int[p];
    for (int i = 0; i < p; i++)
    {
        napraviniz[i] = Convert.ToInt32(z[i])-48;          // pretvaramo string u niz brojeva (vrlo nezgodno, vidi ASCII!)
        Console.Write(napraviniz[i] + " ");
    }
    Console.WriteLine();
    return napraviniz;
}

int[] PrebrojiSlova(string x, string y)
{
    x = x.ToUpper();
    y = y.ToUpper();
    string nizslova = x + y;        //prebacimo sve u velika slova i spojimo u jedan string
    Console.WriteLine(nizslova);

    string s = "";
    int k = nizslova.Length;
    int[] nizbrojeva = new int[k];              //napravimo niz brojeva iste duljine kao taj string
    

    for (int i = 0; i < k; i++)
    {
        nizbrojeva[i] = 1;          // postavimo da svi članovi niza u početku budu 1
    }

    for (int i = 1; i < k; i++)     // gledamo slova s lijeva na desno
    {
        int n = 1;
        while (n <= i)
        {
            if (nizslova[i] == nizslova[i - n])         //ako se npr. 5. slovo podudara s 3. slovom
            {
            nizbrojeva[i - n]++;                    // povećamo 3. član u nizu brojeva za 1
            nizbrojeva[i] = nizbrojeva[i-n];        // i izjednačimo 5. član s 3. članom 
            }
            n++;        // n se povećava dok ne dođemo do slova na 0. mjestu
        }               // kada je to gotovo, prelazimo na sljedeće slovo (i se poveća za 1)
    }
    for (int i = 0; i < k; i++)
    {
        Console.Write(nizbrojeva[i] + " ");     //ispišemo sve članove niza brojeva
        s = s + nizbrojeva[i];
    }
    Console.WriteLine();

    nizbrojeva = StringuNiz(s);

    return nizbrojeva;

}




int[] Sansa(int[] niz)
{
    string t = "";
    int k = niz.Length;
    int m = (k % 2 == 0) ? (k / 2) : (k / 2 + 1);   // ako je duljina niza parna: m=k/2
    int[] noviniz = new int[m];                     // a ako je neparna: m=k/2+1
                                                    // kreiramo novi niz brojeva duljine m
    for (int i = 0; i < m; i++)
    {
        if (i == k - i - 1)         // ovo je srednji član kada je niz neparne duljine
            noviniz[i] = niz[i];      // ne želimo da ga zbroji sam sa sobom
        else
            noviniz[i] = niz[i] + niz[k - i - 1];   //inače zbrajamo vanjske članove
        Console.Write(noviniz[i] + " ");        //  također koristimo for petlju da ispišemo novi niz
        t = t + noviniz[i];
    
    }
    Console.WriteLine();
    noviniz = StringuNiz(t);

    if (noviniz.Length <= 2)
        return noviniz;
    else
        return Sansa(noviniz);




}


Console.Write("Unesi svoje ime:");
string a = Console.ReadLine();

Console.Write("Unesi ime svoje simpatije:");
string b = Console.ReadLine();

int[] c = PrebrojiSlova(a, b);

int[] d = Sansa(c);


Console.WriteLine("Šansa za vašu ljubav je {0}{1} %", d[0], d[1]);


