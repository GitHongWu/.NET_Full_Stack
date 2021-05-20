using System;
using System.Collections.Generic;

namespace Exercise3
{
    
    class HouseholdAccounts
    {
        class Record
        {
            public string Date { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public float Amount { get; set; }
            public Record(string date, string desc, string category, float amount)
            {
                Date = date;
                Description = desc;
                Category = category;
                Amount = amount;
            }
        }

        Dictionary<int, Record> dict;
        public HouseholdAccounts()
        {
            dict = new Dictionary<int, Record>();
        }

        public void Run()
        {
            int option = (int)Options.Exit;
            do
            {
                Console.Clear();
                Menu m = new Menu();
                option = m.Print();
                switch (option)
                {
                    case (int)Options.Add:
                        add();
                        break;
                    case (int)Options.Show:
                        add();
                        break;
                    case (int)Options.Search:
                        add();
                        break;
                    case (int)Options.Modify:
                        add();
                        break;
                    default:
                        option = (int)Options.Exit;
                        break;
                }
                //CustomerFactory factory = new CustomerFactory();
                //BaseCustomer baseCustomer = factory.GetObject(option);
                //if (baseCustomer != null)
                //{
                //    baseCustomer.LogInformation();
                //}
                Console.WriteLine("Press Enter to continue....");
                Console.ReadLine();
            } while (option != (int)Options.Exit);
        }

        public void add()
        {
            Console.WriteLine("Add called");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HouseholdAccounts householdAccounts = new HouseholdAccounts();
            householdAccounts.Run();
        }
    }
}
