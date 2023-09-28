using System.Text.RegularExpressions;
using static System.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        int field;
        int catCurrentPosition, mouseCurrentPosition;
        int catsNextMove, mouseNextMove;

        string curString;

        //
        field = 20;
        catCurrentPosition = 4;
        mouseCurrentPosition = 11;
        bool gameIsContinious = true;
        //
        
        
        
        while (gameIsContinious)
        {
            curString = ReadLine();
            (string, int) player = (Regex.Split(curString, @"\s+")[0], Convert.ToInt32(Regex.Split(curString, @"\s+")[1]));

            switch (player.Item1)
            {
                case "P":
                    OutCurPositions(catCurrentPosition,mouseCurrentPosition);
                    break;
                case "C":
                    catCurrentPosition = (player.Item2 + catCurrentPosition) % field+1;
                    Write(catCurrentPosition);
                    break;
                case "M":
                    mouseCurrentPosition = (player.Item2 + mouseCurrentPosition) % field;
                    break;

                    default:
                        continue;
            }




        }


    }

    public static void OutCurPositions(int catCurrentPosition, int mouseCurrentPosition)
    {
    
    }
}