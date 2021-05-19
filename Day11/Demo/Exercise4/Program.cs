using System;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number a");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number b");
            int b = Convert.ToInt32(Console.ReadLine());
            ArmstrongNum(a,b);
        }
        static public void ArmstrongNum(int a, int b)
        {
            for (int i = a; i <= b; i++)
            {
                int r, sum = 0, temp = i;
                while( temp > 0)
                {
                    r = temp % 10;
                    sum = sum + (r * r * r);
                    temp = temp / 10;
                }
                if(sum == i)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}