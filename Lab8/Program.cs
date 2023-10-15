using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace GeneticsProject
{
    public struct GeneticData
    {
        public string name; //protein name
        public string organism;
        public string formula; //formula
    }

    class Program
    {
        private static StringBuilder sb = new StringBuilder();
        static List<GeneticData> data = new List<GeneticData>();
        static int count = 1;
        static string GetFormula(string proteinName)
        {
            foreach (var item in data)
            {
                if (item.name.Equals(proteinName)) return item.formula;
            }
            return null;
        }
        static void ReadGeneticData(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fragments = line.Split('\t');
                GeneticData protein;
                protein.name = fragments[0];
                protein.organism = fragments[1];
                protein.formula = fragments[2];
                data.Add(protein);
                count++;
            }
            reader.Close();
        }
        static void ReadHandleCommands(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            int counter = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine(); counter++;
                string[] command = line.Split('\t');
                if (command[0].Equals("search"))
                {
                    //001   search  SIIK
                    sb.Append("\n" + $"{counter.ToString("D3")}   {"search"}   {Decoding(command[1])}");
                    int index = Search(Decoding(command[1]));
                    if (index != -1)
                        sb.Append("\n" + $"{data[index].organism}    {data[index].name}");
                    else
                        sb.Append("\n" + "NOT FOUND");
                    sb.Append("\n" + "================================================");
                }
                if (command[0].Equals("diff"))
                {
                    sb.Append("\n" + $"{counter.ToString("D3")}   {"diff"}   {command[1]}\t{command[2]}");
                    string? protein1 = GetFormula(command[1]), protein2 = GetFormula(command[2]);
                    
                    if (protein1 == null || protein2 == null)
                        sb.Append("\n" + $"MISSING {(protein2 == null? command[2]:command[1])}");
                    else
                    {
                        int dif = Diff(Decoding(protein1), Decoding(protein2));
                        sb.Append("\n" + $"amino-acids difference:\n{dif}");
                    }
                    sb.Append("\n" + "================================================");
                }
                if (command[0].Equals("mode"))
                {
                    sb.Append("\n" + $"{counter.ToString("D3")}   {"mode"}\t{command[1]}");
                    string? protein = GetFormula(command[1]);
                    
                    if (protein == null )
                        sb.Append("\n" + "MISSING");
                    else
                    {
                        sb.Append("\n" + "amino-acids occurs:");
                        Mode(Decoding(protein));
                    }
                    sb.Append("\n" + "================================================");
                }
            }
            reader.Close();
        }
        static bool IsValid(string formula)
        {
            List<char> letters = new List<char>() { 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'Y' };
            foreach (char ch in formula)
            {
                if (!letters.Contains(ch)) return false;
            }
            return true;
        }
        static string Encoding(string formula)
        {
            string encoded = String.Empty;
            for (int i = 0; i < formula.Length; i++)
            {
                char ch = formula[i];
                int count = 1;
                while (i < formula.Length-1 && formula[i + 1] == ch)
                {
                    count++;
                    i++;
                }
                if (count > 2) encoded = encoded + count + ch;
                if (count == 1) encoded = encoded + ch;
                if (count == 2) encoded = encoded + ch + ch;

            }
            return encoded;

        }
        static string Decoding(string formula)
        {
            string decoded = String.Empty;
            for(int i=0;i<formula.Length;i++)
            {
                if (char.IsDigit(formula[i]))
                {
                    char letter = formula[i + 1];
                    int conversion = formula[i] - '0';  
                    for (int j = 0; j < conversion-1; j++) decoded = decoded + letter;
                }
                else decoded = decoded + formula[i];
            }
            return decoded;
        }
        static int Search(string amino_acid)
        {
            //       FKIII                FK3I
            string decoded = Decoding(amino_acid);
            for (int i = 0; i < data.Count; i++)
            {
                if (Decoding(data[i].formula).Contains(decoded)) return i;
            }
            return -1;              
        }
        static int Diff(string protein1, string protein2)
        {
            int counter = Math.Abs(protein1.Length - protein2.Length);
            for (int i = 0; i < Math.Min(protein1.Length,protein2.Length); i++)
                if (!protein1[i].Equals(protein2[i])) counter++;
            return counter;
        }
        static void Mode(string protein)
        {
            Dictionary<char, int> pt = new Dictionary<char, int>();
            foreach (var ch in protein)
            {
                if(pt.ContainsKey(ch))
                    pt[ch]++;
                else
                    pt.Add(ch,1);
            }

            int max = 0;
            List<(char, int)> lst = new List<(char, int)>();
            foreach (var VARIABLE in pt)
                if (VARIABLE.Value > max)
                    max = VARIABLE.Value;
            
            foreach (var VARIABLE in pt)
                if (VARIABLE.Value == max)
                    lst.Add((VARIABLE.Key, VARIABLE.Value));
            lst.Sort((x,y) => x.Item1.CompareTo(y.Item1));
            sb.Append("\n" + lst[0].Item1 + "\t" + lst[0].Item2);
        }
        static void Main(string[] args)
        {

            //sb.Append("\n" + Encoding("AAAAAAAATATTTCGCTTTTCAAAAATTGTCAGATGAGAGAAAAAATAAAA"));
            string formula2 =  Decoding("FK3I");
            ReadGeneticData("sequences.0.txt");
            sb.Append("Будревич\n" + "=============Search===================");
            ReadHandleCommands("commands.0.txt");
            using (StreamWriter sw = new StreamWriter(new FileStream("output.txt", FileMode.Create)))
                sw.WriteLine(sb.ToString());
            //sb.Append("\n" + "=============Get Formula of the Protein===================");
            //string formula=GetFormula("6.8 kDa mitochondrial proteolipid");
            //if (formula != null) sb.Append("\n" + formula);
        }
    }
}