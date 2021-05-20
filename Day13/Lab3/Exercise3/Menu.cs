using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    enum Options
    {
        Add = 1,
        Show,
        Search,
        Modify,
        Delete,
        Exit,
    }
    class Menu
    {
        public int Print()
        {
            string[] names = Enum.GetNames(typeof(Options));
            int[] values = (int[])Enum.GetValues(typeof(Options));
            int length = names.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{values[i]}. {names[i]}");
            }
            Console.Write("Enter your choice => ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
