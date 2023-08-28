

namespace _02Delegati
{
    internal class PrimjerKoristenja3
    {
        public PrimjerKoristenja3() 
        {
            var os = new ObradaSmjer();
            Action<Smjer> a = new(metodaOvdje);
            os.IspisSmjerAction(a); //Action, Func i Predicate 
        
        }

        void metodaOvdje(Smjer s)
        {
            Console.WriteLine("\"" + s.Naziv + "\"");
        }
    }
}
