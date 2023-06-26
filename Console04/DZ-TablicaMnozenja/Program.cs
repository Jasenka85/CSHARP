

// DZ
//		Kreiraj program koji će koristeći for petlje
//		automatizirati ispis tablice množenja u ovom obliku:
//		-------------------------------
//		: : :  TABLICA  MNOZENJA  : : :
//		-------------------------------
//		 * |  1  2  3  4  5  6  7  8  9
//		-------------------------------
//		 1 |  1  2  3  4  5  6  7  8  9
//		 2 |  2  4  6  8 10 12 14 16 18
//		 3 |  3  6  9 12 15 18 21 24 27
//		 4 |  4  8 12 16 20 24 28 32 36
//		 5 |  5 10 15 20 25 30 35 40 45
//		 6 |  6 12 18 24 30 36 42 48 54
//		 7 |  7 14 21 28 35 42 49 56 63
//		 8 |  8 16 24 32 40 48 56 64 72
//		 9 |  9 18 27 36 45 54 63 72 81
//		-------------------------------
//		:  :  :  :  :  :   :by Tomislav
//		-------------------------------
//		Umjesto "Ime" treba ispisati ime uneseno s
//		konzole i pri tome pripaziti da zadnje slovo
//		imena bude poravnato s desnim rubom tablice.


Console.Write("Upiši svoje dragocjeno ime: ");
string ime = Console.ReadLine();
string s;
string t;

for (int i = 0; i < 9; i++)
{
    if (i == 0 | i == 2 | i == 4 | i == 6 | i == 8)
    {
        Console.WriteLine("-------------------------------");
    }
    else if (i == 1)
    {
        Console.WriteLine(": : :  TABLICA  MNOZENJA  : : :");
    }
    else if (i == 3)
    {
        Console.Write(" * |");
        for (int k = 1; k < 10; k++)
        {
            Console.Write("  " + k);
        }
        Console.WriteLine();
    }
    
    else if(i==5)
    {
    
        for (int l = 1; l < 10; l++)
        {
            Console.Write(" " + l + " |");
            for (int m = 1; m < 10; m++)
            {
                s = "   " + l * m;
                Console.Write(s[^3..]);
            }
            Console.WriteLine();
        }
    }

    else if (i == 7)
    {
        t = ":  :  :  :  :  :  :  :  :  :  :  :  " + "by " + ime;
        Console.WriteLine(t[^31..]);

    }

}
