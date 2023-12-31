﻿

int ucitajBroj(string s)
{
    while (true)
    {
        int broj;
        Console.Write("Unesi broj " + s + " matrice: ");
        try
        {        
            broj = int.Parse(Console.ReadLine());
            if (broj > 0)
            {
                Console.WriteLine();
                return broj;
            }
            Console.WriteLine("Broj " + s + " mora biti pozitivan!");
            Console.WriteLine();

        }
        catch (FormatException)
        {
            Console.WriteLine("Broj " + s + " mora biti prirodan broj!");
            Console.WriteLine();

        }
        catch (OverflowException)
        {
            Console.WriteLine("Ne pretjeruj!");
            Console.WriteLine();
        }
        catch (Exception)           
        {
            Console.WriteLine("Ooops, nešto si zeznuo. Pokušaj ponovo!");
            Console.WriteLine();
        }
        
    }


}



int redaka = ucitajBroj("redaka");

int stupaca = ucitajBroj("stupaca");

int[,] matrica = new int[redaka, stupaca];

int b = 1;

int n = 0;          // n je dodatni brojač - broj krugova koje smo namotali
                    // kada ispišemo jedan krug brojeva, n se poveća za 1
                    // i sužava područje za 1 sa svih strana
                    // ispisat ću matricu nakon svakog kruga da se vidi kako upisuje brojeve

string s;


while (b < stupaca*redaka)
{

    for (int i = n+1; i <= stupaca-n; i++)      // s desna na lijevo
    {
        if (b <= stupaca * redaka)
            matrica[redaka - n - 1, stupaca - i] = b++;
        else break;
    }

    for (int i = redaka - n-2; i >= n; i--)          // gore
    {
        if (b <= stupaca * redaka)
            matrica[i, n] = b++;
        else break;
    }


    for (int i = n+1; i <= stupaca - n - 1; i++)    // s lijeva na desno
    {
        if (b <= stupaca * redaka)
            matrica[n, i] = b++;
        else break;
    }


    for (int i = n + 1; i <= redaka - n - 2; i++)       // dolje
    {
        if (b <= stupaca * redaka)
            matrica[i, stupaca - n - 1] = b++;
        else break;
    }

    Console.WriteLine("   {0}. krug:", n+1);
    Console.WriteLine();

    for (int i = 0; i < redaka; i++)            // ispisujem matricu nakon svakog kruga i povećam n za 1
    {

        
        for (int j = 0; j < stupaca; j++)
        {
            s = "       " + matrica[i, j];
            Console.Write(s[^5..]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine();

    n++;

}















