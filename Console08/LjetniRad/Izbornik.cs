using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetniRad
{
    internal class Izbornik
    {
        public ObradaSmjer ObradaSmjer { get; }
        public ObradaPolaznik ObradaPolaznik { get; }

        private ObradaGrupa ObradaGrupa;
        public Izbornik()
        {
            ObradaSmjer = new ObradaSmjer();
            ObradaPolaznik = new ObradaPolaznik();
            ObradaGrupa = new ObradaGrupa(this);    //pošaljemo sebe, odnosno Izbornik
            PozdravnaPoruka();
            PrikaziIzbornik();
        }
        private void PozdravnaPoruka()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                        Edunova Console APP v 1.0                                       *");
            
            
        }

        private void PrikaziIzbornik()
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                                            Glavni izbornik:                                            *");
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("\t 1. Smjerovi");
            Console.WriteLine("\t 2. Polaznici");
            Console.WriteLine("\t 3. Grupe");
            Console.WriteLine("\t 4. Izlaz iz programa");
            Console.WriteLine("**********************************************************************************************************");

            switch (Pomocno.ucitajBrojRaspon("\nOdaberite stavku izbornika: ", "Odabir mora biti 1-4", 1, 4))
            {

                case 1:
                    ObradaSmjer.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 2:
                    ObradaPolaznik.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 3:
                    ObradaGrupa.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("\nHvala na korištenju, doviđenja!");
                    break;

            }
                
                
        }
    }
}
