

using System.Reflection.Metadata.Ecma335;

namespace _03Ekstenzije
{
    public static class Ekstenzije  //mora biti public static!
    {
        public static int brojPonavljanja(this string s, char z)   //ova metoda će se moći nakačiti na bilo koji string
        {
            // domaća zadaća:
            //na danom stringu s prebrojiti koliko ima znakova
            //primljenog kroz parametar z
            //npr "Anamarija"  a   vraća 3
            
            return s.Count(b => b == z );
        }

        public static void OdradiPosao(this ISucelje sucelje)
        {
            sucelje.Posao();
        }
    }
}
