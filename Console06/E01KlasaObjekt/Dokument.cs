using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ovo je prvi dio rješenja Z01
namespace E01KlasaObjekt
{
    internal class Dokument
    {
        public Dokument() 
        {
            Console.WriteLine("Prazan");
        }
        public Dokument(string name) 
        { 
            Console.WriteLine("Puni s {0}", name);
        }


    }
}
