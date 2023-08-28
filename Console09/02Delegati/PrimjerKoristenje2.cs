

namespace _02Delegati
{
    internal class PrimjerKoristenje2
    {
    public PrimjerKoristenje2() 
        {
            ObradaSmjer os = new();
            os.IspisSmjer(nijeBitno);
        }

        private void nijeBitno(Smjer s)
        {
            Console.WriteLine("Drugi način: " + s.Naziv);
        }

    }
}
