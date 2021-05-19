using System;

namespace Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Number of Rows:");
            int input = int.Parse(Console.ReadLine());
            int p, lastInt = 0;
            for (int i = 0; i <= input; i++)
            {
                for (p = 1; p <= i; p++)
                {
                    if (lastInt == 1)
                    {
                        Console.Write("0");
                        lastInt = 0;
                    }
                    else if (lastInt == 0)
                    {
                        Console.Write("1");
                        lastInt = 1;
                    }
                }
                Console.Write("\n");
            }
        }
    }
}
