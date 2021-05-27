using System;
using System.Collections.Generic;
using System.Text;
using DataModel;
using DataRepository;

namespace MinionsClientApp
{
    class ManageVillains
    {
        IRepository<Villains> villainsRepository;
        public ManageVillains()
        {
            villainsRepository = new VillainsRepository();
        }

        void AddVillains()
        {
            Villains v = new Villains();
            Console.Write("Enter ID: ");
            v.Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Name: ");
            v.Name = Console.ReadLine();
            Console.Write("Enter EvilnessFactor(good, bad, evil...): ");
            v.EvilnessFactor = Console.ReadLine();

            if (villainsRepository.Insert(v) > 0)
            {
                Console.WriteLine("Villain added");
            }
            else
            {
                Console.WriteLine("Some error has occurred");
            }
        }

        public void Run()
        {
            AddVillains();
        }
    }
}
