using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Serialization;
using static System.Console;
using static System.IO.File;
public class Lab3
{
    static string path = @"C:\Users\makst\Desktop\C#\Labs\Lab3\";
    public static void Main(string[] args)
    {
        using (
            Stream strIn = new FileStream(path + "in.txt", FileMode.Open),
            In = new BufferedStream(strIn, 10)
        ) using (
            Stream strOut = new FileStream(path + "out.txt", FileMode.OpenOrCreate),
            Out = new BufferedStream(strOut, 10)
        )
        {
            try
            {

                In.Read(new byte[3], 0, 3);
                (string, int) number;
                int byt = In.ReadByte();
                StringBuilder nm = new(Convert.ToChar(byt));
                while (byt != -1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        nm.Append(Convert.ToChar(byt));
                        byt = In.ReadByte();
                    }
                    //number = GetNumber(nm);
                    //Console.Write(outNumber(number));
                    WriteLine(nm.ToString());
                    byt = In.ReadByte();
                }
            }
            catch (Exception ignored) { Console.Write(ignored.Message); }
        }

    }
    public static string outNumber((string, int) number)
    {
        StringBuilder sb = new("");
        sb.Append(number.Item1);
        StringBuilder nm = new(Convert.ToString(number.Item2));
        //Console.WriteLine(number.Item2);
        if (number.Item2 < 1000)
        {
            while (nm.Length < 4)
            {
                nm = new StringBuilder("0").Append(nm);
            }
        }
        sb.AppendLine(" " + nm);
        return sb.ToString();
    }


    public static (string, int) GetNumber(StringBuilder num)
    {
        StringBuilder sb = num;
        string[] number;

        number = sb.ToString().Split(" ");
        Write(number[0] + number[1]);

        return (number[0], Convert.ToInt32(number[1]));
    }

    public static string NextNumber(string number)
    {
        return number;
    }


}