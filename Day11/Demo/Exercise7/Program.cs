using System;

namespace Exercise7
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int pin = 123;
            int balance = 0;

            Console.WriteLine("Enter Your Pin Number:");
            int inputPin = Convert.ToInt32(Console.ReadLine());

            while (inputPin != pin)
            {
                Console.WriteLine("Wrong Pin Number, Try Again:");
                inputPin = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("********Welcome to ATM Service**************\n\n1.Check Balance\n\n2.Withdraw Cash\n\n3.Deposit Cash\n\n4.Quit\n\n********************************************\n\nEnter your choice:");
            int userChoice = Convert.ToInt32(Console.ReadLine());

            while (userChoice != 4)
            {
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine(" YOU’RE BALANCE IN Rs: " + balance + "\n\n");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid number, try again");
                        break;
                }
                Console.WriteLine("********Welcome to ATM Service**************\n\n1. Check Balance\n\n2.Withdraw Cash\n\n3.Deposit Cash\n\n4.Quit\n\n********************************************\n\nEnter your choice:");
                userChoice = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
