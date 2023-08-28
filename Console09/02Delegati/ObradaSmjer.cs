

namespace _02Delegati
{
    internal class ObradaSmjer
    {
        public delegate void IspisPozivSmjer(Smjer s);

        private readonly List<Smjer> Smjerovi;
        public ObradaSmjer()
        {
            Smjerovi = new()
            {
            new() { Sifra=1, Naziv="Prvi" },
            new() { Sifra=2, Naziv="Drugi"}
            };

        }

        public void IspisSmjer(IspisPozivSmjer poziv)
        {
            Smjerovi.ForEach(s => poziv(s));
        }

        public void IspisSmjerAction(Action<Smjer> poziv)   
        {
            Smjerovi.ForEach(s => poziv(s));

        }

    }
}
