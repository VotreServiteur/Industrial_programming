using System.Text;
using System.Text.RegularExpressions;
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
                        string n1 = nm.ToString().Split()[0];
                        string n2 = nm.ToString().Split()[1];
                        number = GetNumber(nm);

                        if (new Regex(@"[A-Z]{3}").IsMatch(n1) && (new Regex(@"\d{4}")).IsMatch(n2) && n1.Equals(n1.ToUpper()))
                        //WriteLine(nm.ToString());
                        {
                            //WriteLine(NextNumber(number));
                            strOut.WriteLine(NextNumber(number));
                            
                        }
                        else {
                            strOut.WriteLine(OutNumber(number)+ " ---> invalid ");
                        }
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
        return number.Item1 + " " + number.Item2.ToString("D4");
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