

namespace _01Lambda
{
    internal class Smjer
    {
        public int Sifra { get; set; }

        public String? Naziv { get; set; }      //? označava da može biti null (null safety)
                                                //u slučaju da je null uzet će praznu vrijednost, pa neće baciti exception

        public override string ToString()
        {
            // return Naziv == null? "" : Naziv;
            // krace
            return Naziv ?? ""; //ako nije null, vratit će Naziv, a ako je null vratit će prazan string
        }
    
    
    }
}
