using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(solution(a, b));
        }

        static public int solution(int A, int B)
        {
            int count = 0;
            for (int i = A; i <= B; i++)
            {
                double result = Math.Sqrt(i);
                bool isSquare = result % 1 == 0;
                if (isSquare)
                {
                    //Console.WriteLine(i);
                    count++;
                }
            }
            return count;
        }

    }
}
