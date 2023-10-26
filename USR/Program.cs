// See https://aka.ms/new-console-template for more information

using USR;

internal class Program
{
    public static void Main(string[] args)
    {
        IntArrayClass arr = new(20,0 ,30);
        arr.ShowArray();
        Console.WriteLine($"{arr.Max}\n{arr.Min}\n{arr.Sum}");
        arr++;
        arr.ShowArray();
        Console.WriteLine((int[])arr);
        arr = (IntArrayClass)(int[])arr; 
        Console.WriteLine(!arr);

        arr.Sort();
        arr.ShowArray();
    }
}