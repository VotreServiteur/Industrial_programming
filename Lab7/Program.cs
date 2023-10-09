// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.RegularExpressions;
using static System.Console;
internal class Program
{
    public static void Main(string[] args)
    {
        string[] enc;
        string st;
        int x;
        using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.Open)))
        {
            sr.ReadLine();
            enc = Regex.Split(sr.ReadLine().TrimStart(' '),"\\s+" );
            sr.ReadLine();
            int n = Convert.ToInt32(sr.ReadLine());
            sr.ReadLine();
            //WriteLine(str.Concat(enc));
            StringBuilder stringBuilder = new StringBuilder();
            char k;
            x = n;
            for (int i = 0; i < n; i++)
            {
                k = (char)sr.Read();
                stringBuilder.Append(k == '\r'?"":k);
                if (k == '\r') n++;

            }

            st = stringBuilder.ToString();
        }
        Dictionary<int, int> encrypt = new Dictionary<Int32, Int32>();

        for (int i = 0; i < 20; i++)
        {
            encrypt.Add(i, Convert.ToInt32( enc[i]));
        }
        Dictionary<int, int> encryptSh = new Dictionary<int, int>();
        
        int shortKeys = st.Length % 20 - 1;
        
        string shortSt = st.Substring(st.Length - shortKeys - 1);
        st = st.Substring(0, st.Length - shortKeys - 1);
        //WriteLine(st.Length);
        //WriteLine(shortSt);
        StringBuilder sb1 = new StringBuilder(), sb;
        int l = st.Length / 20;
        char[] cur;
        for (int i = 0; i < l; i++)
        {
            sb = new StringBuilder();
            cur = st.Substring(0, 20).ToCharArray();
            
            st = st.Substring(20);
            for (int j = 0; j < 20; j++)
            {
                sb.Append(cur[encrypt[j]]);
            }

            sb1.Append(sb);
        }
        
        sb = new StringBuilder();
        cur = shortSt.ToCharArray();
        CreateDict(shortKeys,encrypt,ref encryptSh);
        
        for (int j = 0; j < shortKeys; j++)
        {
            sb.Append(cur[encryptSh[j]]);
        }
        sb1.Append(sb);
        
        Write(sb1.ToString());
        using (StreamWriter sw = new StreamWriter(new FileStream("output.txt",FileMode.Create)))
        {
            sw.WriteLine(sb1.ToString());
        }
    }
//0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
//19 14 7  3 11  9 12 13 15  1 16 17 10 18  2  6  4  5  8  0
//l a tl
//    mH.- e
//l.H emt
//  l -la

    public static void CreateDict(int cap,Dictionary<int,int> enc,ref Dictionary<int,int> encSh)
    {
        int curK, curV;
        for (int i = 0; i < cap; i++)
        {
            curK = i;
            curV = enc[curK];

            
            if (curV > cap)
            {
                while (curV > cap)
                {
                   // WriteLine(curV);
                    curV = enc[curV];
                }
                
            }
            //WriteLine(curK+" "+curV);
            encSh.Add(curK,curV);
        }
    }
    
}