using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Delegati
{
    internal class PrimjerKoristenje1
    {
        public PrimjerKoristenje1 () 
        { 
            ObradaSmjer os = new ObradaSmjer ();
            
            os.IspisSmjer(MojIspisUOvojKlasi);

        }

        private void MojIspisUOvojKlasi(Smjer s)
        {
            Console.WriteLine(s.Naziv);
        }
    }
}
