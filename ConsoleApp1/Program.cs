// See https://aka.ms/new-console-template for more information

internal class Program
{
    public static void Main(string[] args)
    {
       wr();
       //Thread.Sleep(3000);
    }

    public static async Task wr()
    {
        string path = "f.txt";
        using(FileStream fs = new(path,FileMode.Create)){ 
            string st = "asdfasdf";
            Console.WriteLine("sda");
            using (StreamWriter ws = new StreamWriter(fs))
            {
                await read();
                ws.Write(st);
                Console.WriteLine("Write");
            }
            Thread.Sleep(5000);
            Console.WriteLine("End");
            
        }
    }

    public async static Task read()
    {
        string st = null;
        while (st.Length < 3){
        using (StreamReader sr = new StreamReader("f.txt"))
            
            {    try
                { st = sr.ReadToEnd(); }
                catch (Exception ign)
                { }}
            Console.WriteLine(st);
        }
    }
}