using System.Text;

namespace USR;
//Будревич
public class IntArrayClass
{
    private int[] intArray;
    private int n;
    public int N => n;
    public int Mult {
        set {
            for (int i = 0; i < intArray.Length; i++)
                intArray[i] *= value; 
        }  }

    public int Max => intArray.Max();
    public int Min => intArray.Min();
    public int Sum => intArray.Sum();

    public int this[int i]
    {
        get => intArray[i];
        set => intArray[i] = value;
    }
    
    public static IntArrayClass operator ++(IntArrayClass arr)
    {
        for (int i = 0; i < arr.N; i++)
        {
            arr[i]++;
        }

        return arr;
    }
    public static IntArrayClass operator --(IntArrayClass arr)
    {
        for (int i = 0; i < arr.N; i++)
        {
            arr[i]--;
        }

        return arr;
    }
    
    public static bool operator !(IntArrayClass arr)
    {
        bool isUp = true;
        for (int i = 1; i < arr.N; i++)
        {
            if (arr[i] < arr[i - 1])
            {
                isUp = false;
            }
        }
        return isUp;
    }
    public static IntArrayClass operator *(IntArrayClass arr, int sk)
    {
        for (int i = 0; i < arr.N; i++)
        {
            arr[i] *= sk;
        }
    
        return arr;
    }

    public static explicit operator int[](IntArrayClass arr)
    {
        return arr.intArray;
    }
    public static explicit operator IntArrayClass(int[] arr)
    {
        var ar = new IntArrayClass(arr.Length);
        ar.intArray = arr;
        return ar;
    }
    
    public IntArrayClass(int n)
    {
        intArray = new int[n];
        this.n = n;
    }
    public IntArrayClass(int n, int a, int b)
    {
        intArray = new int[n];
        this.n = n;
        Random rndm = new Random();
        for (int i = 0; i < n; i++)
            intArray[i] = rndm.Next(a, b);
    }
    
    public IntArrayClass(int a, int b)
    {
        n = b - a;
        intArray = new int[n];
        int j = 0;
        for (int i = a; i <= b; i++)
            intArray[j++] = i;
    }

    public override string ToString()
    {
        StringBuilder sb = new("Elements of Array: ");
        
        foreach (var i in intArray)
            sb.Append(i + " ");
        
        return sb.ToString();
    }

    public void RewriteArray()
    {
        Console.Write("Input element");
        for (int i = 0; i < intArray.Length; i++)
        {
            Console.Write($"\n\t{i}:");
            this[i] = Convert.ToInt32(Console.ReadLine());
        }
    }

    public void ShowArray() => Console.WriteLine(ToString());
    public void Sort()
        {
            for (int i = 0; i < intArray.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < intArray.Length; ++j)
                {
                    if (intArray[j].CompareTo(intArray[min]) < 0)
                    {
                        min = j;
                    }
                }
                (intArray[i], intArray[min]) = (intArray[min], intArray[i]);
            }
        }
}
