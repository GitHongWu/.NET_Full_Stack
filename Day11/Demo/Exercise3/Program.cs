using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your String: ");
            string s = Console.ReadLine();
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(charArray);
        }
    }
}
