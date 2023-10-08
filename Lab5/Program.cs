internal class Program
{
    private static void Main(string[] args)
    {
        ex1();
    }
    public static void ex1()
    {
        using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("input.dat")))
        {
            //foreach(string st in Console.ReadLine()) { 
            //bw.Write();
            
            //bw.Flush();
            //}
        }
        int sum = 0;
        int cur;
        try
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead("input.dat")))
            {
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("output.dat")))
                {
                    cur = br.ReadInt32();
                    br.Read();
                    Console.WriteLine(cur);
                    while (br.PeekChar() != -1)
                    {
                        sum = 0;
                        switch (Math.Sign(cur))
                        {
                            case 0:
                                while (cur == 0 && br.PeekChar() != -1)
                                {
                                    cur = br.ReadInt32();
                                    br.Read();
                                }
                                bw.Write(" " + sum);
                                break;
                            case 1:
                                while (cur > 0 && br.PeekChar() != -1)
                                {
                                    sum += cur;
                                    cur = br.ReadInt32();
                                    br.Read();
                                }
                                bw.Write(" " + sum);

                                break;
                            case -1:
                                while (cur < 0 && br.PeekChar() != -1)
                                {
                                    sum += cur;
                                    cur = br.ReadInt32();
                                    br.Read();
                                }
                                bw.Write(" " + sum);

                                break;
                            default:
                                break;
                        }
                        
                        cur = br.ReadInt32();
                        br.Read();
                        bw.Flush();
                    }
                }
            }
        }
        catch (Exception ign) { }

        }
    }
