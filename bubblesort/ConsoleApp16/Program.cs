using System;

namespace ConsoleApp16
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            n = Convert.ToInt32(Console.ReadLine());
            int[] bubbleArray = new int[n];
            for (int i = 0; i < n; i++)
                bubbleArray[i] = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n-1; i++)
                for (int k = 0; k < n-1; k++)
                {
                    if (bubbleArray[k] < bubbleArray[k+1])
                    {
                        int t = bubbleArray[k];
                        bubbleArray[k] = bubbleArray[k + 1];
                        bubbleArray[k + 1] = t;
                    }
                }
            for (int i = 0; i < n; i++)
                Console.WriteLine(bubbleArray[i]);
        }
    }
}
