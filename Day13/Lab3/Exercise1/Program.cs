using System;

namespace Exercise1
{
    abstract class Shape1
    {

        protected float R, L, B;

        //Abstract methods can have only declarations
        public abstract float Area();
        public abstract float Circumference();

    }

    class Rectangle : Shape1
    {
        public void GetData()
        {
            Console.Write("Enter Length : ");
            L = float.Parse(Console.ReadLine());
            Console.Write("Enter Breadth : ");
            B = float.Parse(Console.ReadLine());
        }
        public void Run()
        {
            GetData();
            Console.WriteLine($"Area : {Area()}");
            Console.Write($"Circumference : {Circumference()}");
        }

        public override float Area()
        {
            return L * B;
        }

        public override float Circumference()
        {
            return 2 * L + 2 * B;
        }
    }

    class Circle:Shape1
    {
        public void GetData()
        {
            Console.Write("Enter Radius : ");
            R = float.Parse(Console.ReadLine());
        }
        public void Run()
        {
            GetData();
            Console.WriteLine($"Area : {Area()}");
            Console.Write($"Circumference : {Circumference()}");
        }

        public override float Area()
        {
            return (float)(Math.Pow(R, 2) * 3.14);
        }

        public override float Circumference()
        {
            return (float)(2 * R * 3.14);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle();
            r.GetData();
            Calculate(r);

            Circle c = new Circle();
            c.GetData();
            Calculate(c);
        }
        public static void Calculate(Shape1 S)
        {
            Console.WriteLine("Area : {0}", S.Area());
            Console.WriteLine("Circumference : {0}", S.Circumference());
        }

    }
}
