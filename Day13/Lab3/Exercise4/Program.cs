using System;

namespace Exercise4
{
    abstract class Person
    {
        private int age;
        public void SetAge(int n)
        {
            age = n;
        }
        public int GetAge()
        {
            return age;
        }
        public void SayHello()
        {
            Console.WriteLine("Hello");
        }
    }

    class Student : Person
    {
        public void GoToClasses()
        {
            Console.WriteLine("I’m going to class.");
        }

        public void ShowAge()
        {
            Console.WriteLine($"My age is: {GetAge()} years old");
        }
    }
    class Teacher : Person
    {
        private string subject;
        public void Explain()
        {
            Console.WriteLine("Explanation begins");
        }
    }
    class StudentAndTeacherTest
    {
        static void Main(string[] args)
        {
            Person p = new Student();
            p.SayHello();

            Student s1 = new Student();
            s1.SetAge(21);
            s1.SayHello();
            s1.ShowAge();

            Teacher t = new Teacher();
            t.SetAge(30);
            t.SayHello();
            t.Explain();
        }
    }
}
