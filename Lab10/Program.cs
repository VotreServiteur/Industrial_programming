using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

internal class Program
{
    public static void Main(string[] args)
    {
        ex3();
    }

    static int Evaluate(string formula)
    {
        Stack<string> stack = new Stack<string>(Regex.Replace(formula, @"[(,)][(),]*\s*"," ").Trim().Split().Reverse());

        return Evaluate(stack);
    }

    static int Evaluate(Stack<string> stack)
    {
        string symbol = stack.Pop();


        if (symbol == "M" || symbol == "m")
        {
            int operand1 = Evaluate(stack);
            int operand2 = Evaluate(stack);

            if (symbol == "M")
                return Math.Max(operand1, operand2);

            return Math.Min(operand1, operand2);
        }

        return Convert.ToInt32(symbol);
    }
    static bool IsOperator(string symbol)
    {
        return symbol == "+" || symbol == "-" || symbol == "*" || symbol == "/";
    }

    public static void ex1()
    {
        int i = 1;
        switch (i)
        {
            case 0:
            {

                Stack<char> st1, st2, st2Rev;
                st1 = new Stack<char>();
                char cur;
                StringBuilder sb = new StringBuilder();
                while ((cur = (char)Read()) != '\r')
                    st1.Push(cur);
                st2 = new Stack<char>();
                Read();
                while ((cur = (char)Read()) != '\r')
                    st2.Push(cur);
                Read();
                bool isRev = true;

                if (st2.Count != st1.Count)
                    isRev = false;
                else
                {
                    st2Rev = new Stack<char>();
                    while (st2.TryPeek(out cur))
                        st2Rev.Push(st2.Pop());

                    while (st1.TryPeek(out cur) && st2Rev.TryPeek(out cur))
                        if (st1.Pop() != st2Rev.Pop())
                            isRev = false;
                }

                if (isRev)
                    WriteLine("Reverse");
                else
                    WriteLine("Not");
                
                break;
            }


            case 1:
            {
                string expression = "* + 3 - 4 3 2";
                Stack<int> stack = new Stack<int>();

                string[] symbols = expression.Split(' ');

                for (int j = symbols.Length - 1; i >= 0; i--)
                {
                    string symbol = symbols[j];

                    if (IsOperator(symbol))
                    {
                        int op1 = stack.Pop();
                        int op2 = stack.Pop();

                        switch (symbol)
                        {
                            case "+":
                                stack.Push(op1 + op2);
                                break;
                            case "-":
                                stack.Push(op1 - op2);
                                break;
                            case "*":
                                stack.Push(op1 * op2);
                                break;
                            case "/":
                                stack.Push(op1 / op2);
                                break;
                        }
                    }
                    else
                    {
                        stack.Push(int.Parse(symbol));
                    }

                }
                WriteLine(stack.Pop());
                break;
            }
            case 2:
            {
                WriteLine(Evaluate(File.ReadAllText("f.txt")));
                break;
            }
            case 3:
            {
                string? st = ReadLine();
                Stack<char> stack = new Stack<char>();
                char p;
                foreach (var cur in st)
                {
                    if (cur == '#')
                    {
                        if (stack.TryPeek(out p)) stack.Pop();
                    }
                    else
                        stack.Push(cur);
                }

                StringBuilder sb = new StringBuilder();
                while (stack.TryPeek(out p))
                {
                    sb.Insert(0, stack.Pop());
                }

                WriteLine(sb.ToString());
                break;
            }
        }
    }

    public static void ex2()
    {
        int i = 3;
        switch (i)
        {
            case 0:
            {
                int a, b;
                a = Convert.ToInt32(ReadLine());
                b = Convert.ToInt32(ReadLine());

                Queue<int> ter1 = new Queue<int>(),
                    ter2 = new Queue<int>(),
                    ter3 = new Queue<int>();
                using (StreamReader sr = new StreamReader("f.txt"))
                {
                    int cur;
                    while (!sr.EndOfStream)
                    {
                        cur = Convert.ToInt32(ReadVal(sr));
                        if (cur < a)
                        {
                            ter2.Enqueue(cur);
                        }
                        else if (cur > b)
                        {
                            ter3.Enqueue(cur);
                        }
                        else
                            ter1.Enqueue(cur);
                    }
                }

                int c;
                Write("[a;b] ");
                while (ter1.TryPeek(out c))
                {
                    Write(ter1.Dequeue() + " ");
                }

                Write("\n< a ");
                while (ter2.TryPeek(out c))
                {
                    Write(ter2.Dequeue() + " ");
                }

                Write("\n> b ");
                while (ter3.TryPeek(out c))
                {
                    Write(ter3.Dequeue() + " ");
                }

                break;
            }
            case 1:
            {
                string gl = "aeyuio";
                Queue<string> ter1 = new Queue<string>(),
                    ter2 = new Queue<string>();

                using (StreamReader sr = new StreamReader("f.txt"))
                {
                    string cur;
                    while (!sr.EndOfStream)
                    {
                        cur = ReadVal(sr);
                        if (gl.Contains(cur.ToLower()[0]))
                        {
                            ter1.Enqueue(cur);
                        }
                        else ter2.Enqueue(cur);
                    }
                }

                string c;
                Write("gl\t");
                while (ter1.TryPeek(out c))
                {
                    Write(ter1.Dequeue() + " ");
                }

                Write("\nsgl\t");
                while (ter2.TryPeek(out c))
                {
                    Write(ter2.Dequeue() + " ");
                }

                break;
            }
            case 2:
            {
                Queue<string> ter1 = new Queue<string>(),
                    ter2 = new Queue<string>();

                using (StreamReader sr = new StreamReader("f.txt"))
                {
                    string cur;
                    while (!sr.EndOfStream)
                    {
                        cur = ReadVal(sr);
                        if (cur[0].Equals(cur.ToUpper()[0]))
                            ter1.Enqueue(cur);
                        else
                            ter2.Enqueue(cur);
                    }
                }

                string c;
                Write("Upper\t");
                while (ter1.TryPeek(out c))
                {
                    Write(ter1.Dequeue() + " ");
                }

                Write("\nLower\t");
                while (ter2.TryPeek(out c))
                {
                    Write(ter2.Dequeue() + " ");
                }

                break;
            }
            case 3:
            {
                Queue<string[]> ter1 = new Queue<string[]>(),
                    ter2 = new Queue<string[]>();

                using (StreamReader sr = new StreamReader("f.txt"))
                {
                    string[] cur;
                    while (!sr.EndOfStream)
                    {
                        cur = ReadEmp(sr);
                        if (Convert.ToInt32(cur[4]) < 30)
                            ter1.Enqueue(cur);
                        else
                            ter2.Enqueue(cur);
                    }
                }

                string[] c;
                Write("Low 30\n");
                while (ter1.TryPeek(out c))
                {
                    Write("\t" + Employee(ter1.Dequeue()));
                }

                Write("\nUp 30\n");
                while (ter2.TryPeek(out c))
                {
                    Write("\t" + Employee(ter2.Dequeue()));
                }

                break;
            }

        }
    }

    public static string Employee(string[] emp)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var s in emp)
        {
            sb.Append(s + " ");
        }

        return sb.Append("\n").ToString();
    }

    public static string[] ReadEmp(StreamReader sr)
    {
        string[] emp = new string[6];

        for (int i = 0; i < 6; i++)
            emp[i] = ReadVal(sr);
        //if(!sr.EndOfStream)sr.Read();
        return emp;
    }

    public static string ReadVal(StreamReader sr)
    {
        StringBuilder sb = new StringBuilder();
        char cur;
        while ((cur = (char)sr.Read()) > ' ' && !sr.EndOfStream)
        {
            sb.Append(cur);
        }

        if (cur == '\r')
        {
            sr.Read();
        }

        return sb.Append(cur).ToString();
    }

    public static void ex3()
    {
        SortedDictionary<string, (int, SortedSet<int>)> dict = new SortedDictionary<string, (int, SortedSet<int>)>();
        using (StreamReader sr = new StreamReader("f.txt"))
        {
            string? cur;
            int n = 0;
            List<string> curLine;
            int N = Convert.ToInt32(ReadLine());
            while (!sr.EndOfStream)
            {
                n++;
                for (int i = 0; i < N; i++)
                {
                    if (sr.EndOfStream)
                    {
                        break;
                    }

                    cur = sr.ReadLine();
                    if (cur.Length > 2)
                    {
                        if (cur[cur.Length - 1] == '\n')
                        {
                            cur = cur.ToLower().Replace("\r\n", "");
                        }

                        curLine = new List<string>(Regex.Split(cur.Trim(new char[] { '?', '.', ',', '!' }).ToLower(), @"[!,.?]*\s"));

                        foreach (var st in curLine)
                        {
                            if (!dict.ContainsKey(st))
                            {
                                dict.Add(st, (1, new SortedSet<int>() { n }));
                            }
                            else
                            {
                                (int, SortedSet<int>) tp = dict[st];
                                tp.Item1++;
                                tp.Item2.Add(n);
                                dict[st] = tp;
                            }
                        }
                    }
                }
            }
        }

        char curChar = dict.Keys.First().ToUpper()[0];
        StringBuilder sb;

        WriteLine(curChar);
        using (StreamWriter sw = new StreamWriter("out.txt"))
        {
            sw.WriteLine(curChar);
            foreach (var key in dict.Keys)
            {
                if (key.ToUpper()[0] != curChar)
                {
                    curChar = key.ToUpper()[0];
                    sw.WriteLine(curChar);
                    WriteLine(curChar);
                }

                sb = new StringBuilder();

                foreach (var num in dict[key].Item2)
                    sb.Append(num + "   ");
                sw.Write($"{key.PadRight(10, '.')}{dict[key].Item1.ToString().PadLeft(10, '.')}: {sb}\n");
                Write($"{key.PadRight(10, '.')}{dict[key].Item1.ToString().PadLeft(10, '.')}: {sb}\n");
            }
        }
    }
}