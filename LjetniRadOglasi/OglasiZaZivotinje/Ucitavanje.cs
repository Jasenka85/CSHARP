﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OglasiZaZivotinje
{
    internal class Ucitavanje
    {
        internal static int UcitajBrojRaspon(string poruka, string greska, int pocetak, int kraj)
        {
            int broj;
            while (true)
            {
                Console.Write(poruka);
                try 
                {
                    broj = int.Parse(Console.ReadLine());
                    if (broj >= pocetak && broj <= kraj)
                    {
                        return broj;
                    }
                    Console.WriteLine(greska);
                }
                catch(Exception e) 
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static int UcitajCijeliBroj(string poruka, string greska)
        {
            int broj;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    broj = int.Parse(Console.ReadLine());
                    if (broj >0)
                    {
                        return broj;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception e)
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
                if (s != null && s.Trim().Length > 0)
                    return s;
                Console.WriteLine(greska);
            }
        }

        internal static DateTime UcitajDatum(string poruka, string greska)
        {
            while (true)
            {
                try
                {
                    Console.Write(poruka);
                    return DateTime.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static bool UcitajBool(string poruka, string greska)
        {
            
            Console.Write(poruka);
            return Console.ReadLine().Trim().ToLower().Equals("da") ? true : false;
               
        }



        internal static string OdrediUlogu(int broj)
        {
            string s;
            switch (broj)
            {
                case 0:
                    s = "korisnik";
                    break;
                case 1:
                    s = "administrator";
                    break;
                case 2:
                    s = "moderator";
                    break;
                default:
                    s = "";
                    break;
            }
            return s;

        }

        internal static string OdrediKategoriju(int broj)
        {
            string s;
            switch (broj)
            {
                
                case 1:
                    s = "Poklanjam životinju";
                    break;
                case 2:
                    s = "Želim udomiti životinju";
                    break;
                default:
                    s = "";
                    break;
            }
            return s;

        }

        internal static string OdrediAktivnost(bool stanje)
        {
            string s;
            switch (stanje)
            {

                case true:
                    s = "Aktivan";
                    break;
                case false:
                    s = "Neaktivan";
                    break;
                default:
                    s = "";
                    break;
            }
            return s;

        }
    }
}
