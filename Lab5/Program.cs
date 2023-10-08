using System.Text;
using static System.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        ex4();
    }
    /*
 * записать все числа в файл, затем вывести на экран все компоненты файла, меньшие
заданного числа;
• вывести на экран все положительные компоненты файла;
• вычислить среднее арифметическое компонентов файла, стоящих на четных позициях.
 */
public static void ex1()
    {
        using (
            BinaryWriter bw  = new BinaryWriter(new FileStream("input.dat",FileMode.Create))
            )
        {
            bw.Write("12 30 -219 38 -2 49 0 123");
        }

        int k = 1;
        double cur;
        double sum = 0;
        
        Write("Введите число:\n\t");
        double max = Convert.ToDouble(ReadLine());
        using (
            BinaryWriter bw = new BinaryWriter(new FileStream("output.dat",FileMode.Create))
            )
        using (
            BinaryReader br = new BinaryReader(new FileStream("input.dat", FileMode.Open))
            )
        {
            br.Read();
            while (br.PeekChar() != -1)
            {
                cur = ReadDouble(br);
                if (k++ % 2 == 0) sum += cur;
                if (cur > 0) Write($"{cur} ");
                if (cur < max) bw.Write(cur+" "); 
            }
            Write($"\nСреднее: {sum / k}");
        }
    }

    private static double ReadDouble(BinaryReader br)
    {
        StringBuilder sb = new StringBuilder("");
        while (br.PeekChar() != ' ' && br.PeekChar() != -1)
        {
            sb.Append((char)br.Read());
        }
        br.Read();
        return Convert.ToDouble(sb.ToString());
    }
/*
 найти в файле f2.dat число, наиболее близкое по величине к минимальному значению в
файле f2.dat;
• определить в каком из файлов больше положительных, отрицательных и нулевых
значений;
• определить, являются ли числа в файле f1.dat упорядоченными по возрастанию;
• определить, образуют ли числа в файле f1.dat знакопеременную последовательность
чисел*/ 
    public static void ex2()
    {
        using (
            BinaryWriter bw  = new BinaryWriter(new FileStream("f1.dat",FileMode.Create))
        )
        {
            bw.Write("12 -30 35 -399 0"); // Пусть ноль подходит под условие знакопеременности
        }
        using (
            BinaryWriter bw  = new BinaryWriter(new FileStream("f2.dat",FileMode.Create))
        )
        {
            bw.Write("45 834 64 37 245 -2");
        }

        (int, int, int) f1Count = (0,0,0); // для подсчёта кол-ва пол, отр, и нулей в первом
        (int, int, int) f2Count = (0,0,0); // во втором файле
        
        int f2Min = Int32.MaxValue;//минимальное во втором файле
        
        int lastF1 = Int32.MinValue;
        int cur;
       
        using (
            BinaryReader br = new BinaryReader(new FileStream("f2.dat", FileMode.Open))
        )
        {
            br.Read(); 
            while (br.PeekChar() != -1)
            {
                cur = ReadInt(br);
                
                if (f2Min > cur) f2Min = cur;
                
                switch (Math.Sign(cur))
                {
                    case 0:
                        f2Count.Item3++;
                        break;
                    case 1:
                        f2Count.Item1++;
                        break;
                    case -1:
                        f2Count.Item2++;
                        break;
                }
            }
        }
    
        bool isUp = true;
        bool isSgn = true;
        
        (int, int) min;
        using (
            BinaryReader br = new BinaryReader(new FileStream("f1.dat", FileMode.Open))
        )
        {
            br.Read();
            
            int k = 1;
            
            cur = ReadInt(br);

            min = (cur, Math.Abs(cur - f2Min)); // для поиска ближайшего

            while (br.PeekChar() != -1)
            {
                
                lastF1 = cur;
                
                if (k % 2 == 0)
                {
                    if (cur > 0)
                        isSgn = false;
                }
                else
                {
                    if(cur < 0)
                        isSgn = false;
                    
                }
                
                switch (Math.Sign(cur))
                {
                    case 0:
                        f1Count.Item3++;
                        break;
                    case 1:
                        f1Count.Item1++;
                        break;
                    case -1:
                        f1Count.Item2++;
                        break;
                }

                cur = ReadInt(br);
                if(Math.Abs(cur-f2Min) < min.Item2) min = (cur, Math.Abs(cur - f2Min));
                WriteLine(lastF1 + " " + cur);
                if (lastF1 > cur) isUp = false;
                k++;
            }
        }
        WriteLine(isUp);
        WriteLine(
            $"Минимальное число во втором файле - {f2Min}\nНаиболее близкое в первом - {min.Item1}\nБольше всего" + 
            $"\n\tположительных в{(f1Count.Item1 > f2Count.Item1 ? " первом" : "о втором")} файле" + //Не стал делать вариант с одинаковым количеством т.к
            $"\n\tотрицательных в{(f1Count.Item2 > f2Count.Item2 ? " первом" : "о втором")} файле" + //занимает много места и создаёт много вложений,
            $"\n\tнулевых в{(f1Count.Item2 > f2Count.Item2 ? " первом" : "о втором")} файле" +       //но лежит на поверхности
            $"\nПервая последовательность {(isUp?"":"не")}упорядоченная и {(isSgn?"":"не")}знакопеременная"
            );
    }
    
    public static void ex3()
    {
        using (BinaryWriter bw = new BinaryWriter(new FileStream("input.dat",FileMode.Create)))
        {
            bw.Write(Console.ReadLine());
        }
        int sum = 0;
        int cur;
        try
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead("input.dat")))
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("output.dat")))
                {
                    br.Read();
                    cur = ReadInt(br);
                    while (br.PeekChar() != -1)
                    {
                        sum = 0;
                        
                        switch (Math.Sign(cur))
                        {
                            case 0:
                                while (cur == 0 && br.PeekChar() != -1)
                                {
                                    cur = ReadInt(br);
                                }
                                break;
                            
                            case 1:
                                while (cur > 0 && br.PeekChar() != -1)
                                {
                                    sum += cur;
                                    cur = ReadInt(br);
                                }
                                break;
                            
                            case -1:
                                while (cur < 0 && br.PeekChar() != -1)
                                {
                                    sum += cur;
                                    cur = ReadInt(br);
                                }
                                break;
                        }
                        bw.Write(sum + " ");
                       
                    }
                }
            }
        }
        catch (Exception ign) { Console.WriteLine(ign);}

        }

    public static int ReadInt(BinaryReader br)
    {
        StringBuilder sb = new StringBuilder("");
        char cur;
        while (br.PeekChar() != ' ' && br.PeekChar() != -1)
        {
            sb.Append((char)br.Read());
        }

        br.Read();
        return Convert.ToInt32(sb.ToString());
    }

    public static void ex4()
    {
        using (
            BinaryWriter bw  = new BinaryWriter(new FileStream("f1.dat",FileMode.Create))
        )
        {
            bw.Write("1 3 5 12 12 13 15 17 19");
        }
        using (
            BinaryWriter bw  = new BinaryWriter(new FileStream("f2.dat",FileMode.Create))
        )
        {
            bw.Write("2 4 5 6 8 9 10 12");
        }
        
        using (
            BinaryReader f1r  = new BinaryReader(new FileStream("f1.dat",FileMode.Open))
        ) 
            using (
                BinaryReader f2r  = new BinaryReader(new FileStream("f2.dat",FileMode.Open))
            )
                using (
                    BinaryWriter bw = new BinaryWriter(new FileStream("f3.dat", FileMode.Create))
                )
                {
                    f1r.Read();
                    f2r.Read();
                    
                    int? cur1, cur2;
                    while (f1r.PeekChar() != -1 || f2r.PeekChar() != -1)
                    {
                        if (f1r.PeekChar() != -1)
                            cur1 = ReadInt(f1r);
                        else
                            cur1 = null;
                        
                        if (f2r.PeekChar() != -1)
                            cur2 = ReadInt(f2r);
                        else 
                            cur2 = null;
                        
                        WriteLine(cur1 + " " + cur2);
                        
                        if (cur1 == null)
                            bw.Write(cur2 + " ");
                        else if (cur2 == null)
                            bw.Write(cur1 + " ");
                        
                        else {
                            if (cur1 > cur2)
                            {
                                while (cur1 > cur2 && f2r.PeekChar() != -1)
                                {
                                    bw.Write(cur2 + " ");
                                    cur2 = ReadInt(f2r);
                                }
                                bw.Write(cur1 + " ");
                                bw.Write(cur2 + " ");
                            }
                            else
                            {
                                while (cur1 < cur2 && f1r.PeekChar() != -1)
                                {
                                    bw.Write(cur1 + " ");
                                    cur1 = ReadInt(f1r);
                                }
                                bw.Write(cur2 + " ");
                                bw.Write(cur1 + " ");
                            }
                        } 
                    }
        }
    }
}
