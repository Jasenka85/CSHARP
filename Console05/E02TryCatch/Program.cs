
int ucitajBroj()
{
    while (true)
    {
        Console.Write("Unesi broj: ");
        try
        {
            // ovdje uvijek dođe
            // tu stavljamo kod za kojeg pretpostavljamo da će dati iznimku
            return int.Parse(Console.ReadLine());      // ako se dogodi iznimka, on se prebacuje u catch i ispisuje "Ne može"
                                                       // ako korisnik unese broj, on se ispiše i izlazimo iz beskonačne petlje
        }
        catch (FormatException e)   // za slučaj da ne unese broj, nego npr. slovo
                                    // varijabla e hvata podatke o iznimci
        {
            // ovdje dođe ako se dogodila iznimka
            Console.WriteLine("Ne može!");

        }
        catch (OverflowException)   // za slučaj da unese prevelik broj
        {
            Console.WriteLine("Ne pretjeruj!");
        }
        catch (Exception)           //u slučaju da se dogodila neka druga iznimka
        {
            Console.WriteLine("Ooops, nešto nije dobro...");
        }
        finally
        {
            //ovdje uvijek dođe

        }
    }



}




int i = ucitajBroj();
int j = ucitajBroj();

Console.WriteLine(i + j);
