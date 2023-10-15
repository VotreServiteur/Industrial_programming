// See https://aka.ms/new-console-template for more information

internal class Program
{
    public static async Task Main(string[] args)
    {
       wr();
       //Thread.Sleep(3000);
    }

    public static async Task wr()
    {
        string path = "f.txt";
        await using(FileStream fs = new(path,FileMode.Create)){ 
            string st = "asdfasdf";
            Console.WriteLine("sda");
            using(StreamReader rs = new StreamReader(fs))
            using (StreamWriter ws = new StreamWriter(fs))
            {
                ws.Write(st);
                Console.WriteLine("Write");
                Console.WriteLine(await rs.ReadLineAsync());
            }
            
            Console.WriteLine("End");
            
        }
    }
}