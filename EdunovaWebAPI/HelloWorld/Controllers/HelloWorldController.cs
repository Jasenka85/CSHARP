
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello world";
        }

        [HttpGet]
        [Route("pozdrav")]

        public string DrugaMetoda()
        {
            return "Pozdrav svijetu";
        }

        [HttpGet]
        [Route("pozdravParametar")]

        public string DrugaMetoda(string s)
        {
            return "Hello " + s;
        }

        [HttpGet]
        [Route("pozdravViseParametara")]

        public string DrugaMetoda(string s="", int i=0)
        {
            return "Hello " + s + " " + i;
        }

        // Kreirajte rutu /HelloWorld/zad1
        // koja ne prima parametre i vraća vaše ime

        [HttpGet]
        [Route("zad1")]
        public string MojeIme()
        {
            return "Jasenka Augustinović";
        }


        //Kreirajte rutu /HelloWorld/zad2
        //Koja prima dva broja i vraća njihov zbroj


        [HttpGet]
        [Route("zad2")]
        public int Zbroj(int b1, int b2)
        {
            return b1+b2;
        }


        //Kreirajte rutu /HelloWorld/zad3
        //koja prima parametar brojPonavljanja
        //Ruta vraća niz znakova "Osijek" koji ima
        //onoliko elemenata koliko smo primili u brojPonavljanja


        [HttpGet]
        [Route("zad3")]

        public string[] VolimOsijek(int brojPonavljanja)
        {
            var nizOsijeka = new string[brojPonavljanja];

            for (int i=0; i<brojPonavljanja; i++)
            {
                nizOsijeka[i] = "Osijek";
            }

            return nizOsijeka;
        }


        //DZ kreirati metodu /HelloWorld/ciklicna
        //koja prima dva parametra (x i y) a vraca
        //ciklicnu matricu kao dvodimenzionalni niz brojeva


        [HttpGet]
        [Route("ciklicna")]

        public string[] IspisiCiklicnuMatricu(int redaka, int stupaca)
        {
            return CiklicnaMatrica.NapraviMatricu(redaka, stupaca);
        
        }


    }
}
