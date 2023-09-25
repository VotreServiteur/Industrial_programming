using System.Text;
using static System.Console;
public class Lab
{
    static string path = @"C:\Users\makst\Desktop\C#\Labs\Lab3\";
    public static void Main(string[] args)
    {
        using (
            Stream strIn = new FileStream(@".\in.txt", FileMode.OpenOrCreate),
            In = new BufferedStream(strIn, 10)
        ) using (
            StreamWriter strOut = new StreamWriter(@".\out.txt")
        )
        {

            try
            {

                strOut.WriteLine("Programmer: Budrevich \n");
                (string, int) number;
                int k = 0;
                int byt = In.ReadByte();
                StringBuilder nm;
                while (byt != -1)
                {
                    nm = new("");
                    if (byt >= 'A')
                    { while (byt > '\n'){
                        nm.Append(Convert.ToChar(byt));
                        byt = In.ReadByte();
                    }
                    //WriteLine(nm.ToString());
                    number = GetNumber(nm);
                    //WriteLine(NextNumber(number));
                    strOut.WriteLine(NextNumber(number));
                    k++;
                    }
                    byt = In.ReadByte();
                }
                strOut.WriteLine($"\n{k} plates was processed");
            }
        catch (Exception ignored) { Console.Write(ignored.Message); }
        }

    }
    public static string OutNumber((string, int) number)
    {
        StringBuilder sb = new("");
        sb.Append(number.Item1);
        StringBuilder nm = new(Convert.ToString(number.Item2));
        if (number.Item2 < 1000)
        {
            while (nm.Length < 4)
            {
                nm = new StringBuilder("0").Append(nm);
            }
        }
        sb.Append(" " + nm);
        return sb.ToString();
    }



    public static (string, int) GetNumber(StringBuilder num)
    {
        StringBuilder sb = num;
        string[] number;
        number = sb.ToString().Split(" ");
        return (number[0], Convert.ToInt32(number[1]));
    }

    public static string NextNumber((string, int) number)
    {
        StringBuilder message = new(OutNumber(number));
        if (number == ("ZZZ", 9999)) return OutNumber(number) + " ---> Final number" ;
        if (number.Item2 == 9999)
        {
            number.Item2 = 0;

            number.Item1 = GetNextAlpha(number.Item1);
        }
        else number.Item2++;

        message.Append(" ---> " + OutNumber(number));
        return message.ToString();
    }


    public static string GetNextAlpha(string alpha)
    {
        char[] lets = alpha.ToCharArray(); 
        
        if (lets[2] == 'Z')
        {
            lets[2] = 'A';
            if (lets[1] == 'Z')
            {
                lets[1] = 'A';
                if (lets[0] == 'Z')
                {
                    lets[0] = 'A';
                }
                else lets[0]++;

            }
            else lets[1]++;

        }
        else lets[2]++;
        return "" + lets[0] + lets[1] + lets[2];
    }

}