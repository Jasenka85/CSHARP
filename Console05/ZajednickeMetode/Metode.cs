

namespace ZajednickeMetode
{
    public class Metode
    {

        public void ispis()
        {
            Console.WriteLine("Hello");
        }

        public void ispis(string poruka)        //dva ista naziva metode, ali jedan ne prima, a drugi prima parametar
        {                                       // to se zove method overload
            Console.WriteLine(poruka);          // npr Console.WriteLine ima 18 različitih verzija, ovisno o tome kakve parametre prima
        }


        public void ispis(int poruka)        //napravili smo treću vrstu, koja prima int
        {
            Console.WriteLine(poruka);
        }


        //ova metoda se može pozvati iz same klase

        /// <summary>   to je dokumentacija metode
        /// Metoda zbraja dva primljena broja
        /// </summary>
        /// <param name="a">Prvi primljeni broj</param>
        /// <param name="b">Drugi primljeni broj</param>
        /// <returns>Zbroj dvaju primljenih brojeva</returns>
        public static int izracunaj(int a, int b)   // static jer ne mora ništa pamtiti u memoriji
        {
            return a + b;
        }

        public static void ispisiMatricu(int[,] matrica)
        {
            for (int i = 0; i < matrica.GetLength(0); i++)      //GetLength(0) daje broj redaka
            {
                for (int j = 0; j < matrica.GetLength(1); j++)        //GetLength(1) daje broj stupaca
                {
                    Console.Write(matrica[i, j] + " ");
                }
                Console.WriteLine();
            }
            string s = "";
            for (int i = 0; i < (matrica.GetLength(1) * 2) - 1; i++)      //hoćemo da ispiše zvjezdice nakon svake matrice
            {
                s += "*";
            }
            Console.WriteLine(s);
        }


        public static int faktorijel(int broj)
        {
            if (broj == 1)
            {
                return broj;            // računa tek kada se ispuni uvjet prekida
            }                           // prvo samo naslaže 5*4*3*2*1
            return broj * faktorijel(broj - 1);

        }



        
    }
}






