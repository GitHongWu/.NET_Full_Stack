using System;

namespace MinionsClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Queries q = new Queries();
            q.PrintAllMinionsByVillain();

            Console.Write("\nEnter the Villain Id to print all Minions: ");
            q.GetMinionsByVillainId(Convert.ToInt32(Console.ReadLine()));
        }
    }
}
