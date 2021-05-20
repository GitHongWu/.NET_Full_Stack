using System;

namespace Exercise5
{
    class ComplexNumber
    {
        public int real { get; set; }
        public int imaginary { get; set; }
        public ComplexNumber(int _real, int _imaginary)
        {
            real = _real;
            imaginary = _imaginary;
        }
        public void SetImaginary(int n)
        {
            imaginary = n;
        }
        public override string ToString()
        {
            return $"({real}, {imaginary})";
        }
        public double GetMagnitude()
        {
            return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
        }
        public void Add(ComplexNumber other)
        {
            real += other.real;
            imaginary += other.imaginary;
            //return new ComplexNumber(real + other.real, imaginary + other.imaginary);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool debug = false;

            ComplexNumber number = new ComplexNumber(5, 2);
            Console.WriteLine("Number is: " + number.ToString());

            number.SetImaginary(-3);
            Console.WriteLine("Number is: " + number.ToString());

            Console.Write("Magnitude is: ");
            Console.WriteLine(number.GetMagnitude());

            ComplexNumber number2 = new ComplexNumber(-1, 1);
            number.Add(number2);
            Console.Write("After adding: ");
            Console.WriteLine(number.ToString());

            if (debug)
                Console.ReadLine();
            Console.ReadKey();


        }
    }
}
