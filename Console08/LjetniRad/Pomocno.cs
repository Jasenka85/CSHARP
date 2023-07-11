using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class Pomocno
    {
        public static int ucitajBrojRaspon(string poruka, string greska, int poc, int kraj)
        {
            int b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b >= poc && b <= kraj)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);

                }
                catch
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static int ucitajCijeliBroj(string poruka, string greska)
        {
            int b;
            while (true)  
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b >0)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);

                }
                catch
                {
                    Console.WriteLine(greska);
                }

            }
        }

        internal static string UcitajString(string poruka, string greska)
        {
            string s = "";
            while (true)
            {
                Console.Write(poruka);
                s = Console.ReadLine();
                if (s!=null && s.Trim().Length > 0)
                {
                    return s;
                }
                Console.WriteLine(greska);
            }
        }
    }
}
