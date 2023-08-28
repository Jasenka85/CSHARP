using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Ekstenzije
{
    internal class Smjer : Entitet, ISucelje
    {
       

        public String? Naziv { get; set; }

        public void Posao()
        {
            Console.WriteLine("Radim posao na Smjeru");
        }
    }
}
