using System.Text.RegularExpressions;
using static System.Console;
internal class Program
{
    private static void Main(string[] args)
    {
       
        int? catCurrentPosition = null, mouseCurrentPosition = null;
        
       
        bool gameIsContinious = true;
        bool mouseCathed = false;
        int? mouseCathedPosition = null;
        int catDistance = 0, mouseDistance = 0;
        //
        using (StreamReader sr = new("ChaseData.txt")) using (StreamWriter sw = new("PursuitLog.txt"))
        {
            try
            {

                int field = Convert.ToInt32(sr.ReadLine().Trim());
                string curString;

                sw.WriteLine($"Cat and Mouse\nCat Mouse  Distance\n-------------------");

                while (gameIsContinious)
                {
                    curString = sr.ReadLine();
                    if (curString == null) { break; }
                    (string, int) player = (Regex.Split(curString, @"\s+")[0], Regex.Split(curString, @"\s+").Length > 1 ? Convert.ToInt32(Regex.Split(curString, @"\s+")[1]) : 0);

                    switch (player.Item1)
                    {
                        case "P":
                            OutCurPositions(sw, catCurrentPosition, mouseCurrentPosition);

                            break;
                        case "C":
                            if (catCurrentPosition != null)
                            {
                                catDistance += Math.Abs(player.Item2);
                                catCurrentPosition = player.Item2 < 0 ? (player.Item2 + catCurrentPosition + field) % field : (player.Item2 + catCurrentPosition) % field + (player.Item2 <= field ? 0 : 1);
                            }
                            else catCurrentPosition = player.Item2;
                           
                            WriteLine(catDistance);
                            break;
                        case "M":
                            if (mouseCurrentPosition != null)
                            {
                                mouseDistance += Math.Abs(player.Item2);
                                mouseCurrentPosition = player.Item2 < 0 ? (player.Item2 + mouseCurrentPosition + field) % field : (player.Item2 + mouseCurrentPosition) % field + (player.Item2 <= field ? 0 : 1);
                            }
                            else mouseCurrentPosition = player.Item2;
                            break;

                        default:
                            continue;
                    }
                    
                    if (mouseCurrentPosition == catCurrentPosition) { gameIsContinious = false; mouseCathed = true; mouseCathedPosition = mouseCurrentPosition; } 
                
                }

            }
            
            catch { }
            
            finally {
                
                sw.WriteLine("-------------------" + '\n');
                sw.WriteLine("Distance traveled:   Mouse     Cat");
                sw.WriteLine("                    " + '\t' + mouseDistance + "\t\t" + catDistance);
                
                if (mouseCathed)
                {
                    sw.Write("Mouse cought at:" + mouseCathedPosition);
                }
                else
                {
                    sw.Write("Mouse was not cought");
                }

            } }
    }

    public static void OutCurPositions(StreamWriter sw, int? catCurrentPosition, int? mouseCurrentPosition)
    {
        sw.Write((catCurrentPosition == null ? "??" : catCurrentPosition.ToString()) + '\t');
        sw.Write((mouseCurrentPosition == null ? "??" : mouseCurrentPosition.ToString()) + '\t');
        sw.Write(((catCurrentPosition == null || mouseCurrentPosition == null)? "  " : Math.Abs(Convert.ToDecimal(catCurrentPosition - mouseCurrentPosition)).ToString()) + '\n');

    }
}