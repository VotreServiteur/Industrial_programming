using System.Text;
using System.Text.RegularExpressions;
using static System.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        ex2();
    }
    static void ex1()
    { 
        string sb = (Console.ReadLine());

        Write(Regex.Replace(sb, @"[^A-Z,a-z]", "").Length);
        StringBuilder sb = new(Regex.Replace(sb, @"[^A-Z,a-z]", ""));
        Write(sb.Insert(sb.Length - 1, "ok"));
    }
    static void ex2()
    {
        string x = ReadLine();
        
        Write(Regex.Replace(Console.ReadLine(), @$"\s\w*{x}\W", ""));
    }
    static void ex3()
    {
        string[] x = ReadLine().ToLower().Split();
        int n = 0;
        foreach (string s in x)
        {
            foreach (char c in s.ToCharArray())
            {
                //Write(c - 'а' + 1 + " ");
                n += c - 'а' + (c >'е'? 2: 1);
            }
        }
        //Console.WriteLine(n);
        while (n > 9)
        {
            char[] k = n.ToString().ToCharArray();
            n = 0;
            foreach (char c in k)
            { 
                n += c - '1' + 1;
            }

        }
        Console.WriteLine(n);
    }
    static void ex4()
    {
        int k = Convert.ToInt32(ReadLine());

        char[] arr = ReadLine().ToCharArray();
        foreach (char c in arr)
        {
            if(c >= 'А' && c <= 'Я')
            {
                Write((char)((c + k - 'А') % ('Я' - 'А' + 1) - 1 + 'Б'));
            }
            else if(c >= 'а' && c <= 'я')
            {

                Write((char)((c + k - 'а') % ('я' - 'а' + 1) - 1 + 'б'));
            }
            else Write(c);
        }

    }
}