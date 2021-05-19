using System;

namespace Exercise2
{
    class Arithmetic
    {
        int a, b;
        public void GetNumber()
        {
            Console.WriteLine("Enter number a");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number b");
            b = Convert.ToInt32(Console.ReadLine());
        }
        public int Addition() { return a + b; }
        public int Subtraction() { return a - b; }
        public int Multiplication() { return a * b; }
        public int Division() { return a / b; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Arithmetic a1 = new Arithmetic();
            a1.GetNumber();

            Console.WriteLine("Choose\n1: Addition\n2: Subtraction\n3: Multiplication\n4: Division");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(a1.Addition());
                    break;
                case 2:
                    Console.WriteLine(a1.Subtraction());
                    break;
                case 3:
                    Console.WriteLine(a1.Multiplication());      
                    break;
                case 4:
                    Console.WriteLine(a1.Division());
                    break;
                default:
                    break;
            }
        }
    }
}
