using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string filePath = "test.txt";
        string textToWrite = "Привет, мир!";

        await WriteTextAsync(filePath, textToWrite);

        string readText = await ReadTextAsync(filePath);
        Console.WriteLine(readText);
    }

    static async Task WriteTextAsync(string filePath, string text)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        await writer.WriteLineAsync(text);
    }

    static async Task<string> ReadTextAsync(string filePath)
    {
        using StreamReader reader = new StreamReader(filePath);
        return await reader.ReadLineAsync();
    }
}