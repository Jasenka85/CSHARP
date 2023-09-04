namespace HelloWorld.Controllers
{
    public class CiklicnaMatrica
    {

        
        public static int[,] NapraviMatricu(int redaka, int stupaca)
        {


            int[,] matrica = new int[redaka, stupaca];

            int b = 1;

            int n = 0;


            while (b < stupaca * redaka)
            {

                for (int i = n + 1; i <= stupaca - n; i++)      // s desna na lijevo
                {
                    if (b <= stupaca * redaka)
                        matrica[redaka - n - 1, stupaca - i] = b++;
                    else break;
                }

                for (int i = redaka - n - 2; i >= n; i--)          // gore
                {
                    if (b <= stupaca * redaka)
                        matrica[i, n] = b++;
                    else break;
                }


                for (int i = n + 1; i <= stupaca - n - 1; i++)    // s lijeva na desno
                {
                    if (b <= stupaca * redaka)
                        matrica[n, i] = b++;
                    else break;
                }


                for (int i = n + 1; i <= redaka - n - 2; i++)       // dolje
                {
                    if (b <= stupaca * redaka)
                        matrica[i, stupaca - n - 1] = b++;
                    else break;
                }

                n++;

            }

            
            return matrica;

        }


        public static string[] NapraviNizStringova(int redaka, int stupaca)
        {

            int[,] matrica = new int[redaka, stupaca];

            matrica = NapraviMatricu(redaka, stupaca);

            var nizRedaka = new string[redaka];


            for (int i = 0; i < redaka; i++)
            {
                string s = "";
                for (int j = 0; j < stupaca; j++)
                {
                    string t = "     " + matrica[i, j];
                    t = t[^5..];
                    s = s + t;
                }
                nizRedaka[i] = s;
            }

            return nizRedaka;

        }

    }
}
