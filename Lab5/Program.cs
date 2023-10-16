using System.Text;
using System.Text.RegularExpressions;
using static System.Console;
using StreamReader = System.IO.StreamReader;

internal class Program
{
    private static void Main(string[] args)
    {
        ex1();
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
            BinaryWriter bw = new BinaryWriter(new FileStream("input.dat", FileMode.Create))
        )
        {
            bw.Write("12 30 -219 38 -2 49 0 123");
        }

        int k = 1;
        double cur;
        double sum = 0;

        Write("Введите число:\n\t");
        double max = Convert.ToDouble(ReadLine());
        using (BinaryWriter bw = new BinaryWriter(new FileStream("output.dat", FileMode.Create)))
        using (BinaryReader br = new BinaryReader(new FileStream("input.dat", FileMode.Open)))
        {
            br.Read();
            while (br.PeekChar() != -1)
            {
                cur = ReadDouble(br);
                if (k++ % 2 == 0) sum += cur;
                if (cur > 0) Write($"{cur} ");
                if (cur < max) bw.Write(cur + " ");
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
        using (BinaryWriter bw = new BinaryWriter(new FileStream("f1.dat", FileMode.Create)))
        {
                   bw.Write("12 -30 35 -399 0"); // Пусть ноль подходит под условие знакопеременности
        }

        using (
            BinaryWriter bw = new BinaryWriter(new FileStream("f2.dat", FileMode.Create))
        )
        {
            bw.Write("45 834 64 37 245 -2");
        }

        (int, int, int) f1Count = (0, 0, 0); // для подсчёта кол-ва пол, отр, и нулей в первом
        (int, int, int) f2Count = (0, 0, 0); // во втором файле

        int f2Min = Int32.MaxValue; //минимальное во втором файле

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
                    if (cur < 0)
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
                if (Math.Abs(cur - f2Min) < min.Item2) min = (cur, Math.Abs(cur - f2Min));
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
            $"\n\tнулевых в{(f1Count.Item2 > f2Count.Item2 ? " первом" : "о втором")} файле" + //но лежит на поверхности
            $"\nПервая последовательность {(isUp ? "" : "не")}упорядоченная и {(isSgn ? "" : "не")}знакопеременная"
        );
    }

    public static void ex3()
    {
        using (BinaryWriter bw = new BinaryWriter(new FileStream("input.dat", FileMode.Create)))
        {
            bw.Write(Console.ReadLine());
        }

        int sum = 0;
        int cur;
        try
        {
            using BinaryReader br = new BinaryReader(File.OpenRead("input.dat"));
            using BinaryWriter bw = new BinaryWriter(File.OpenWrite("output.dat"));
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
        catch (Exception ign)
        {
            Console.WriteLine(ign);
        }

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
            BinaryWriter bw = new BinaryWriter(new FileStream("f1.dat", FileMode.Create))
        )
        {
            bw.Write("1 3 5 12 12 13 15 17 19");
        }

        using (
            BinaryWriter bw = new BinaryWriter(new FileStream("f2.dat", FileMode.Create))
        )
        {
            bw.Write("2 4 5 6 8 9 10 12");
        }

        using (
            BinaryReader f1r = new BinaryReader(new FileStream("f1.dat", FileMode.Open))
        )
        using (
            BinaryReader f2r = new BinaryReader(new FileStream("f2.dat", FileMode.Open))
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

                else
                {
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

    public static void ex5()
    {
        char[] sent = ReadLine().ToCharArray();
        using (StreamWriter wr = new StreamWriter(new FileStream("output.txt", FileMode.Create)))
        {
            foreach (var ch in sent)
            {
                if (!Regex.IsMatch(ch.ToString(), "\\d")) wr.Write(ch.ToString());
            }
        }

        using (StreamReader sr = new StreamReader("output.txt"))
        {
            WriteLine(sr.ReadToEnd());
        }
    }

/*
  вывести на экран все слова, которые начинаются на заданную букву.
• вывести на экран все слова, длина которых равна заданному числу.
• вывести на экран все слова, которые начинаются и заканчиваются одной буквой.
• вывести на экран все слова, которые начинаются на ту же букву, что и последнее слово.
 */

    public static void ex6()
    {
        using (StreamWriter wr = new StreamWriter(new FileStream("input.txt", FileMode.Create)))
        {
            wr.Write(
                "Duis a nulla accumsan felis tincidunt egestae Ut egestas elit id lorem tincidunt a iaculis ante scelerisque Cras gravida velit faucibus" +
                " faucibus placerat nisi dui dignissim mauris a aliquet lorem leo sit amet nisl Vivamus dui justo imperdiet non ernare");
        }

        StringBuilder sb;
        Write($"Введите букву: ");
        char let = ReadLine().ToCharArray()[0];

        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {

            while (!sr.EndOfStream)
            {
                sb = new StringBuilder();
                char st;
                while ((st = (char)sr.Read()) != ' ' && !sr.EndOfStream)
                {
                    sb.Append(st);
                }

                if (sb.ToString().ToLower().StartsWith(let)) Write(sb + " ");
            }
        }

        Write("\nВведите длину: ");
        int len = Convert.ToInt32(ReadLine());

        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            while (!sr.EndOfStream)
            {
                sb = new StringBuilder();
                char st;
                while ((st = (char)sr.Read()) != ' ' && !sr.EndOfStream)
                {
                    sb.Append(st);
                }

                if (sb.ToString().Length == len) Write(sb + " ");

            }
        }

        WriteLine();
        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            sb = new StringBuilder();
            while (!sr.EndOfStream)
            {
                sb = new StringBuilder();
                char st;
                while ((st = (char)sr.Read()) != ' ' && !sr.EndOfStream)
                {
                    sb.Append(st);
                }

                if (sb.ToString().ToLower().StartsWith(sb[sb.Length - 1])) Write(sb + " ");
                let = sb[0];
            }
        }

        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            WriteLine();
            while (!sr.EndOfStream)
            {
                sb = new StringBuilder();
                char st;
                while ((st = (char)sr.Read()) != ' ' && !sr.EndOfStream)
                {
                    sb.Append(st);
                }

                if (sb.ToString().ToLower().StartsWith(let)) Write(sb + " ");
            }
        }
    }

    public static void ex7()
    {
        using (StreamWriter wr = new StreamWriter(new FileStream("input.txt", FileMode.Create)))
        {
            wr.Write(
                "Duis a nulla accumsan felis tincidunt egestae Ut egestd\n elit id lorem tincidunt a iaculis ante scelerisque\n Cras gravida velit faucibus" +
                " faucibus placerat\n nisi dui dignissim\n mauris a aliquet lorem leo sit amet nisl Vivamus dui justo imperdiet non ernare");
        }

        StringBuilder sb;
        Write($"Введите букву: ");
        char let = ReadLine().ToCharArray()[0];
        int k = 1;
        int es = 0;
        (string, int /*lenght*/, int /*num*/) longSt;
        (string, int) shortSt;
        bool stW = false;

        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            string st = sr.ReadLine();

            longSt = (st, st.Length, k++);
            shortSt = (st, st.Length);

            if (st.ToLower().StartsWith(st.ToCharArray()[st.Length - 1]))
                es++;
            if (st.ToLower().StartsWith(let))
            {
                stW = true;
                WriteLine(st);
            }

            while (!sr.EndOfStream)
            {
                st = sr.ReadLine();
                k++;

                if (st.Length > longSt.Item2) longSt = (st, st.Length, k);
                if (st.Length < shortSt.Item2) shortSt = (st, st.Length);

                if (st.ToLower().StartsWith(st.ToCharArray()[st.Length - 1])) es++;

                if (st.ToLower().StartsWith(let))
                {
                    stW = true;
                    WriteLine(st);
                }

            }

            Write(
                $"Количество строк первого условия - {es}\nСамая длинная строка - {longSt.Item1}\nЕё\tдлина -" +
                $" {longSt.Item2}\n\tномер - {longSt.Item3}\nСамая корткая - {shortSt.Item1}\nЕё длина - {shortSt.Item2}" +
                $"\n{(stW ? "" : $"Строки начинающейся с буквы {let} не найдено")}");
        }
    }

    public static void ex8()
    {

        using (StreamWriter wr = new StreamWriter(new FileStream("input.txt", FileMode.Create)))
        {
            wr.Write(
                "Duis a nulla accumsan felis tincidunt egestae Ut egestd\n elit id lorem tincidunt a iaculis ante scelerisque\n Cras gravida velit faucibus" +
                " faucibus placerat\n nisi dui dignissim\n mauris a aliquet lorem leo sit amet nisl Vivamus dui justo imperdiet non ernare"
            );
        }

        Write("Input len ");
        int len = Convert.ToInt32(ReadLine());
        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            using (StreamWriter sw1 = new StreamWriter(new FileStream("output1.txt", FileMode.Create)))
            using (StreamWriter sw2 = new StreamWriter(new FileStream("output2.txt", FileMode.Create)))
            using (StreamWriter sw3 = new StreamWriter(new FileStream("output3.txt", FileMode.Create)))
            using (StreamWriter sw4 = new StreamWriter(new FileStream("output4.txt", FileMode.Create)))
            {
                while (!sr.EndOfStream)
                {
                    var st = sr.ReadLine();
                    sw1.WriteLine(st + " " + st.Length + " " + st.Replace(" ", "").Length);
                    if (st.Length > len) sw2.WriteLine(st);
                    if (st.Length % 2 == 0) sw3.WriteLine(st);
                    sw4.WriteLine(DeleteOdd(st));
                }

            }

        }
    }

    public static void ex9()
    {
        using (var sw = new StreamWriter(new FileStream("input.txt", FileMode.Create)))
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i == j) sw.Write(1 + " ");
                    else sw.Write(0 + " ");
                }

                sw.WriteLine();
            }
        }

        Write("Input column: ");
        int m = Convert.ToInt32(ReadLine());
        bool isE = true, isZ = true, isU = true;
        int last = 0;
        using (var sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        using (var sw1 = new StreamWriter(new FileStream("out1.txt", FileMode.Create)))
        using (var sw2 = new StreamWriter(new FileStream("out2.txt", FileMode.Create)))
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb = new StringBuilder();
                char[] st = string.Concat(sr.ReadLine().Split()).ToCharArray();
                isZ = true;
                if (last > st[m]) isU = false;
                last = st[m];
                for (int j = 0; j < 100; j++)
                {
                    if (isE)
                        if (j == i) isE = st[j] == '1';
                        else isE = st[j] == '0';

                    if (st[j] != 0) isZ = false;

                }

                sw1.WriteLine(st[m]);
                if (isZ) WriteLine("Zero line was found!");
                else
                {
                    foreach (char ch in st)
                    {
                        sb.Append(ch + " ");
                    }

                    sw2.WriteLine(i + " " + sb);
                }

            }
        }

        WriteLine($"Matr is {(isE ? "" : "not ")}unit\nColumn {m} is {(isU ? "" : "not ")}sorted");
    }

    private static string DeleteOdd(string st)
    {
        StringBuilder sb = new StringBuilder();
        char[] str = st.ToCharArray();

        for (int i = 0; i < str.Length; i += 2)
        {
            sb.Append(str[i]);
        }

        return sb.ToString();
    }



}