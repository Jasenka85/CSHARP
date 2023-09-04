namespace HelloWorld.Controllers
{
    public class LjubavniKalkulator
    {
        public static int[] StringuNiz(string z)
        {
            int p = z.Length;
            int[] napraviniz = new int[p];
            for (int i = 0; i < p; i++)
            {
                napraviniz[i] = Convert.ToInt32(z[i]) - 48;          
            }
            return napraviniz;
        }

        public static int[] PrebrojiSlova(string x, string y)
        {
            x = x.ToUpper();
            y = y.ToUpper();
            string nizslova = x + y;        
            
            string s = "";
            int k = nizslova.Length;
            int[] nizbrojeva = new int[k];       

            for (int i = 0; i < k; i++)
            {
                nizbrojeva[i] = 1;          
            }

            for (int i = 1; i < k; i++)     
            {
                int n = 1;
                while (n <= i)
                {
                    if (nizslova[i] == nizslova[i - n])     
                    {
                        nizbrojeva[i - n]++;                    
                        nizbrojeva[i] = nizbrojeva[i - n];         
                    }
                    n++;       
                }               
            }

            for (int i = 0; i < k; i++)
            {
                s = s + nizbrojeva[i];
            }

            nizbrojeva = StringuNiz(s);

            return nizbrojeva;

        }




        public static int[] Sansa(int[] niz)
        {
            string t = "";
            int k = niz.Length;
            int m = (k % 2 == 0) ? (k / 2) : (k / 2 + 1);   
            int[] noviniz = new int[m];                     
                                                            
            for (int i = 0; i < m; i++)
            {
                if (i == k - i - 1)         
                    noviniz[i] = niz[i];      
                else
                    noviniz[i] = niz[i] + niz[k - i - 1];   
                       
                t = t + noviniz[i];

            }
            
            noviniz = StringuNiz(t);

            if (noviniz.Length <= 2)
                return noviniz;

            else
                return Sansa(noviniz);

        }


        

        

    }
}
